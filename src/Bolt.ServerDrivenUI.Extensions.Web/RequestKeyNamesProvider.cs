using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web;

internal sealed class RequestKeyNamesProvider : IRequestKeyNamesProvider
{
    private readonly IOptions<CustomRequestDataKeys> _options;
    private readonly Lazy<RequestKeyNames> _keyNames;

    public RequestKeyNamesProvider(IOptions<CustomRequestDataKeys> options)
    {
        _options = options;
        _keyNames = new Lazy<RequestKeyNames>(Load);
    }

    private RequestKeyNames Load()
    {
        return new RequestKeyNames
        {
            Mode = _options.Value.Mode ?? DefaultRequestDataKeys.Mode,
            App = _options.Value.App ?? DefaultRequestDataKeys.App,
            Device = _options.Value.Device ?? DefaultRequestDataKeys.Device,
            Platform = _options.Value.Platform ?? DefaultRequestDataKeys.Platform,
            CorrelationId = _options.Value.CorrelationId ?? DefaultRequestDataKeys.CorrelationId,
            RootApp = _options.Value.RootApp ?? DefaultRequestDataKeys.RootApp,
            SectionNames = _options.Value.SectionNames ?? DefaultRequestDataKeys.SectionNames,
            LayoutVersionId = _options.Value.LayoutVersionId ?? DefaultRequestDataKeys.LayoutVersionId,
            ScreenSize = _options.Value.ScreenSize ?? DefaultRequestDataKeys.ScreenSize,
            Tenant = _options.Value.Tenant ?? DefaultRequestDataKeys.Tenant,
            RootRequestUri = _options.Value.RootRequestUri ?? DefaultRequestDataKeys.RootRequestUri
        };
    }
    
    public RequestKeyNames Get() => _keyNames.Value;
}