using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpRequestHeadersProvider
{
    IEnumerable<(string Key, string Value)> Get(IRequestContextReader context);
}