using Ensemble.Core;
using Microsoft.Extensions.Options;

namespace Ensemble.Web;

public class RequestKeyNamesProvider : IRequestKeyNamesProvider
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
            App = _options.Value.App ?? DefaultRequestDataKeys.App,
            Device = _options.Value.Device ?? DefaultRequestDataKeys.Device,
            Platform = _options.Value.Platform ?? DefaultRequestDataKeys.Platform,
            Id = _options.Value.Id ?? DefaultRequestDataKeys.Id,
            RootId = _options.Value.RootId ?? DefaultRequestDataKeys.RootId,
            RootApp = _options.Value.RootApp ?? DefaultRequestDataKeys.RootApp,
            SectionNames = _options.Value.SectionNames ?? DefaultRequestDataKeys.SectionNames,
            LayoutVersionId = _options.Value.LayoutVersionId ?? DefaultRequestDataKeys.LayoutVersionId,
        };
    }
    
    public RequestKeyNames Get() => _keyNames.Value;
}