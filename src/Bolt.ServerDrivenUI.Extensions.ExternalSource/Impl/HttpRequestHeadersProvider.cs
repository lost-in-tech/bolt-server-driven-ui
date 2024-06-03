using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpRequestHeadersProvider(
    IAppInfoProvider appInfoProvider,
    IRequestKeyNamesProvider requestKeyNamesProvider) : IHttpRequestHeadersProvider
{
    public IEnumerable<(string Key, string Value)> Get(IRequestContextReader context)
    {
        var app = appInfoProvider.Get();
        var keyNames = requestKeyNamesProvider.Get();
        var requestData = context.RequestData();

        if (requestData.ScreenSize.HasValue)
        {
            yield return (keyNames.ScreenSize, requestData.ScreenSize.Value.ToString());
        }

        yield return (keyNames.App, app.Name);
        yield return (keyNames.RootApp, requestData.RootApp);
        yield return (keyNames.CorrelationId, requestData.CorrelationId);
        yield return (keyNames.Tenant, requestData.Tenant);

        if (requestData.AuthToken != null)
        {
            yield return (keyNames.AuthToken, requestData.AuthToken);
        }

        if (requestData.RootRequestUri != null)
        {
            yield return (keyNames.RootRequestUri, Uri.EscapeDataString(requestData.RootRequestUri.ToString()));
        }

        if (requestData.Device.HasValue)
        {
            yield return (keyNames.Device, requestData.Device.Value.ToString());
        }

        if (requestData.Platform.HasValue)
        {
            yield return (keyNames.Platform, requestData.Platform.Value.ToString());
        }

        if (requestData.Tags.Length > 0)
        {
            yield return (keyNames.Tags, string.Join(",", requestData.Tags));
        }
        
        if (!string.IsNullOrWhiteSpace(requestData.Lang))
        {
            yield return (keyNames.Lang, requestData.Lang);
        }
    }
}