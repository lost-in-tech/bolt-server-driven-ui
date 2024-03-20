namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

internal interface IHttpClientWrap
{
    Task<HttpResponseMessage> SendAsync(string serviceName, 
        HttpRequestMessage msg, 
        CancellationToken ct);
}

internal sealed class HttpClientWrap(IHttpClientFactory httpClientFactory) : IHttpClientWrap
{
    public Task<HttpResponseMessage> SendAsync(string serviceName, HttpRequestMessage msg, CancellationToken ct)
    {
        var client = httpClientFactory.CreateClient(serviceName);

        return client.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead, ct);
    }
}