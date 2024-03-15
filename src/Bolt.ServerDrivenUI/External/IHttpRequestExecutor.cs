namespace Bolt.ServerDrivenUI.External;

public interface IHttpRequestExecutor
{
    Task<HttpResponseMessage> Execute(HttpRequestMessage message, CancellationToken ct);
}

internal sealed class HttpRequestExecutor : IHttpRequestExecutor
{
    private readonly IHttpClientFactory _clientFactory;

    public HttpRequestExecutor(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public Task<HttpResponseMessage> Execute(HttpRequestMessage message, CancellationToken ct)
    {
        var http = _clientFactory.CreateClient();
        return http.SendAsync(message, ct);
    }
}