using Bolt.MaySucceed;
using Ensemble.Core;
using Ensemble.Extensions.Web.RazorParser;
using Microsoft.Extensions.Options;

namespace Ensemble.Extensions.Web.LayoutProviders;

internal sealed class RazorWideLayoutProvider<TRequest>(IRazorXmlViewParser xmlViewParser,
    IOptions<RazorLayoutProviderSettings> options) : ILayoutProvider<TRequest>
{
    private static readonly SemaphoreSlim Lock = new(1, 1);
    private static MaySucceed<LayoutResponse>? Response;
    
    public async Task<MaySucceed<LayoutResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        if (Response != null) return Response.Value;

        await Lock.WaitAsync(ct);

        try
        {
            if (Response != null) return Response.Value;

            Response = await ReadLayout(context, request, ct);
        }
        finally
        {
            Lock.Release();
        }

        return Response.Value;
    }
    
    private async Task<MaySucceed<LayoutResponse>> ReadLayout(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var settings = options.Value ?? new RazorLayoutProviderSettings();
        
        var rsp = await xmlViewParser.Read<TRequest>(new()
        {
            RootFolder = settings.RootFolder,
            ViewFolder = settings.ViewFolder,
            ViewModel = request,
            ViewName = $"Layout.{RequestScreenSize.Wide}"
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
        
        if (requestData.ScreenSize is not RequestScreenSize.Compact)
        {
            return requestData.IsSectionOnlyRequest() == false;
        }

        return false;
    }
}