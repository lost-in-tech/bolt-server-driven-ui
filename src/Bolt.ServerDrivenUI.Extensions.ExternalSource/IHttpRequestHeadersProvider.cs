using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpRequestHeadersProvider
{
    Dictionary<string, string> Get(IRequestContextReader context);
}