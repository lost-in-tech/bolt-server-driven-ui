using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpRequestMessageBuilder(
        IHttpRequestUrlBuilder requestUrlBuilder,
        IEnumerable<IHttpRequestHeadersProvider> headerProviders,
        IRequestContextReader requestContext) 
    : IHttpRequestMessageBuilder
{
    public HttpRequestMessage Build(HttpMethod httpMethod,
        string url, 
        IEnumerable<(string Key, string? Value)>? queryStrings,
        IEnumerable<(string Key, string? Value)>? headers)
    {
        var urlWithQuery = requestUrlBuilder.Build(url, queryStrings);
        
        var msg = AppendHeaders(new HttpRequestMessage(httpMethod, urlWithQuery));

        if (headers == null) return msg;
        
        foreach (var header in headers)
        {
            if(header.Value == null) continue;
                
            msg.Headers.Add(header.Key, header.Value);
        }

        return msg;
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