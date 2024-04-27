using System.Security.Cryptography;
using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

internal sealed class RazorWideLayoutProvider<TRequest>(IRazorXmlViewParser xmlViewParser,
    IOptions<RazorLayoutProviderSettings> options,
    IEnumerable<ILayoutFileNameProvider<TRequest>> fileNameProviders) : ILayoutProvider<TRequest>
{
    private static readonly SemaphoreSlim Lock = new(1, 1);
    private static MaySucceed<LayoutResponse>? Response;

    public async Task<MaySucceed<LayoutResponse>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct)
    {
        var rsp = await GetInternal(context, request, ct);

        if (rsp.IsFailed) return rsp;

        var requestVersionId = context.RequestData().LayoutVersionId;

        if (!string.IsNullOrWhiteSpace(requestVersionId)
            && string.Equals(requestVersionId, rsp.Value.VersionId, StringComparison.OrdinalIgnoreCase))
        {
            return new LayoutResponse
            {
                Element = EmptyElement.Instance,
                Name = rsp.Value.Name,
                VersionId = rsp.Value.VersionId,
                NotModified = true
            };
        }

        return rsp;
    }
    
    private async Task<MaySucceed<LayoutResponse>> GetInternal(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var fileName = fileNameProviders.GetFileNameApplied(context, request);
        
        var existing = LayoutStore.TryGet<TRequest>(fileName);

        if (existing != null) return existing;
        
        await Lock.WaitAsync(ct);

        try
        {
            existing = LayoutStore.TryGet<TRequest>(fileName);
            
            if (existing != null) return existing;

            var rsp = await ReadLayout(context, request, fileName, ct);

            if (rsp.IsSucceed)
            {
                LayoutStore.Set<TRequest>(fileName, rsp.Value);

                return rsp;
            }

            return rsp.Failure;
        }
        finally
        {
            Lock.Release();
        }
    }
    
    private async Task<MaySucceed<LayoutResponse>> ReadLayout(IRequestContextReader context, TRequest request, string fileName, CancellationToken ct)
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
            Name = RequestScreenSize.Wide.ToString().ToLowerInvariant(),
            VersionId = version ?? settings.Version ?? Guid.NewGuid().ToString()
        };
    }

    public bool IsApplicable(IRequestContextReader context, TRequest request)
    {
        var requestData = context.RequestData();

        return requestData.ScreenSize is not RequestScreenSize.Compact;
    }
}