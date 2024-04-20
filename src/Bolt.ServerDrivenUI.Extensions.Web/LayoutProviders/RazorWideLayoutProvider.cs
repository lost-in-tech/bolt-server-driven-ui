using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

internal sealed class RazorWideLayoutProvider<TRequest>(IRazorXmlViewParser xmlViewParser,
    IOptions<RazorLayoutProviderSettings> options,
    IEnumerable<ILayoutFileNameProvider<TRequest>> fileNameProviders) : ILayoutProvider<TRequest>
{
    private static readonly SemaphoreSlim Lock = new(1, 1);
    private static MaySucceed<LayoutResponse>? Response;
    
    public async Task<MaySucceed<LayoutResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var fileName = fileNameProviders.GetFileNameApplied(context, request);
        var requestVersion = context.RequestData().LayoutVersionId ?? string.Empty;
        
        var existing = LayoutStore.TryGet<TRequest>(fileName, requestVersion);

        if (existing != null) return existing;
        
        await Lock.WaitAsync(ct);

        try
        {
            existing = LayoutStore.TryGet<TRequest>(fileName, requestVersion);
            
            if (existing != null) return existing;

            var rsp = await ReadLayout(context, request, fileName, ct);

            if (rsp.IsSucceed)
            {
                LayoutStore.Set<TRequest>(fileName, rsp.Value.VersionId ?? Guid.NewGuid().ToString(), rsp.Value);

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
            VersionId = settings.Version ?? Guid.NewGuid().ToString()
        };
    }

    public bool IsApplicable(IRequestContextReader context, TRequest request)
    {
        var requestData = context.RequestData();

        return requestData.ScreenSize is not RequestScreenSize.Compact;
    }
}