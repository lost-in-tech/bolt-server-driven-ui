using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpRequestMessageBuilder(
        IHttpRequestUrlBuilder requestUrlBuilder,
        IEnumerable<IHttpRequestHeadersProvider> headerProviders,
        IRequestContextReader requestContext) 
    : IHttpRequestMessageBuilder
{
    public HttpRequestMessage Build(RequestMessageBuilderInput input)
    {
        var urlWithQuery = requestUrlBuilder.Build(input.Path, input.QueryStrings);
        
        var msg = AppendHeaders(new HttpRequestMessage(input.Method, urlWithQuery));

        if (input.Headers == null) return msg;
        
        foreach (var header in input.Headers)
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
