using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.External;

public interface IHttpRequestHeadersProvider
{
    Dictionary<string, string> Get(IRequestContextReader context);
}