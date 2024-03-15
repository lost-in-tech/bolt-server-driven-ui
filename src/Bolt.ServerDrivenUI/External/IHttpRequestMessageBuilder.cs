using System.Text;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.External;

public interface IHttpRequestMessageBuilder
{
    HttpRequestMessage Build(string url, IEnumerable<(string Key, string? Value)>? queryStrings = null);
}

internal sealed class HttpRequestMessageBuilder(
        IHttpRequestUrlBuilder requestUrlBuilder,
        IEnumerable<IHttpRequestHeadersProvider> headerProviders,
        IRequestContextReader requestContext) 
    : IHttpRequestMessageBuilder
{
    public HttpRequestMessage Build(string url, IEnumerable<(string Key, string? Value)>? queryStrings)
    {
        var urlWithQuery = requestUrlBuilder.Build(url, queryStrings);
        
        return AppendHeaders(new HttpRequestMessage
        {
            RequestUri = new Uri(urlWithQuery)
        });
    }

    private HttpRequestMessage AppendHeaders(HttpRequestMessage message)
    {
        foreach (var headerProvider in headerProviders)
        {
            var headers = headerProvider.Get(requestContext);

            foreach (var header in headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }
        }

        return message;
    }
}