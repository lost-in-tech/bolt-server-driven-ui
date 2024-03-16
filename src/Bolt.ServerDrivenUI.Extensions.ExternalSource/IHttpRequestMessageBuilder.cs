using System.Text;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpRequestMessageBuilder
{
    HttpRequestMessage Build(HttpMethod method, string url, 
        IEnumerable<(string Key, string? Value)>? queryStrings = null,
        IEnumerable<(string Key, string? Value)>? headers = null);
}