using System.Collections.Concurrent;
using System.Security.Cryptography;
using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

internal sealed class RazorLayoutProvider<TRequest>(IRazorXmlViewParser xmlViewParser,
    IOptions<RazorLayoutProviderSettings> options,
    IEnumerable<ILayoutFileNameProvider<TRequest>> fileNameProviders) : ILayoutProvider<TRequest>
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public async Task<MaySucceed<IReadOnlyCollection<LayoutResponse>>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct)
    {
        var rsp = await GetInternal(context, request, ct);

        if (rsp.IsFailed) return rsp;

        var layoutVersions = context.RequestData().LayoutVersions;

        var result = new List<LayoutResponse>();
        
        foreach (var item in rsp.Value)
        {
            if (layoutVersions != null
                && item.VersionId != null
                && layoutVersions.TryGetValue(item.VersionId, out var layout))
            {
                result.Add(new LayoutResponse
                {
                    Element = EmptyElement.Instance,
                    Name = item.Name,
                    VersionId = item.VersionId,
                    NotModified = true
                });
            }
            else
            {
                result.Add(item);
            }
        }

        return result;
    }
    
    private async Task<MaySucceed<IReadOnlyCollection<LayoutResponse>>> GetInternal(IRequestContextReader context, 
        TRequest request, CancellationToken ct)
    {
        var layoutFileNames = fileNameProviders.GetLayoutFileNames(context, request);
        var result = new List<LayoutResponse>();
        var tasks = new List<Task<MaySucceed<LayoutResponse>>>();

        foreach (var layoutFileName in layoutFileNames)
        {
            var existing = LayoutStore.TryGet<TRequest>(layoutFileName.FileName);

            if (existing != null)
            {
                result.Add(existing);
                continue;
            }
            
            tasks.Add(GetInternal(layoutFileName.LayoutName, layoutFileName.LayoutName, context, request, ct));   
        }

        if (tasks.Count > 0)
        {
            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                if (task.Result.IsSucceed)
                {
                    result.Add(task.Result.Value);
                }
            }
        }

        return result;
    }

    private async Task<MaySucceed<LayoutResponse>> GetInternal(string layoutName, string layoutFileName, IRequestContextReader context,
        TRequest request, CancellationToken ct)
    {
        var semaphoreSlim = _locks.GetOrAdd(layoutName, _ => new SemaphoreSlim(1, 1));
            
        await semaphoreSlim.WaitAsync(ct);

        try
        {
            var existing = LayoutStore.TryGet<TRequest>(layoutName);

            if (existing != null)
            {
                return existing;
            }

            var rsp = await ReadLayout(context, request, layoutName, layoutFileName, ct);

            if (rsp.IsSucceed)
            {
                LayoutStore.Set<TRequest>(layoutFileName, rsp.Value);

                return rsp;
            }

            return rsp.Failure;
        }
        finally
        {
            semaphoreSlim.Release();
        }    
    }
    
    private async Task<MaySucceed<LayoutResponse>> ReadLayout(IRequestContextReader context, 
        TRequest request,
        string name,
        string fileName, 
        CancellationToken ct)
    {
        var settings = options.Value ?? new RazorLayoutProviderSettings();
        
        var viewPath = string.Format(TypeViewLocations.Get<TRequest>(settings.RootFolder, settings.ViewFolder), $"Layout.{fileName}");
        viewPath = viewPath.Replace("~/", string.Empty);

        string version;
        using (var md5 = MD5.Create())
        {
            await using (var sr = File.OpenRead(viewPath))
            {
                version = Convert.ToBase64String(await md5.ComputeHashAsync(sr, ct));
            }
        }
        
        var rsp = await xmlViewParser.Read<TRequest>(new()
        {
            RootFolder = settings.RootFolder,
            ViewFolder = settings.ViewFolder,
            ViewModel = request,
            ViewName = $"Layout.{fileName}"
        });

        if (rsp.IsFailed) return rsp.Failure;

        return new LayoutResponse
        {
            Element = rsp.Value,
            Name = name.ToLowerInvariant(),
            VersionId = version ?? settings.Version ?? Guid.NewGuid().ToString()
        };
    }

    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}