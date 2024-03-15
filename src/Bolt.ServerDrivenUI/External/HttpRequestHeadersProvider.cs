using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.External;

internal sealed class HttpRequestHeadersProvider
    (IAppInfoProvider appInfoProvider, IRequestKeyNamesProvider requestKeyNamesProvider) : IHttpRequestHeadersProvider
{
    public Dictionary<string, string> Get(IRequestContextReader context)
    {
        var app = appInfoProvider.Get();
        var keyNames = requestKeyNamesProvider.Get();
        var requestData = context.RequestData();
        
        var result = new Dictionary<string, string>();

        if (requestData.ScreenSize.HasValue)
        {
            result[keyNames.ScreenSize] = requestData.ScreenSize.Value.ToString();
        }

        result[keyNames.App] = app.Name;
        result[keyNames.RootApp] = requestData.RootApp;
        result[keyNames.CorrelationId] = requestData.CorrelationId;
        result[keyNames.Tenant] = requestData.Tenant;

        if (requestData.Device.HasValue)
        {
            result[keyNames.Device] = requestData.Device.Value.ToString();
        }

        if (requestData.Platform.HasValue)
        {
            result[keyNames.Platform] = requestData.Platform.Value.ToString();
        }

        return result;
    }
}

public abstract class HttpRequestHeadersProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    public abstract string[] ForSections { get; }
    
    public virtual bool IsLazy(IRequestContextReader context, TRequest request, CancellationToken ct) => false;
    public Task<MaySucceed<ScreenSectionResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

