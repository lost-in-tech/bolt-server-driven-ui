using Microsoft.AspNetCore.Http;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public interface IHttpRequestWrapper
{
    Uri? RequestUri();
    string Header(string name);
    string Query(string name);
    string RootTraceId();
    string TraceId();
    bool IsAuthenticated();
    string? UserId();
}

internal sealed class HttpRequestWrapper(IHttpContextAccessor httpContextAccessor) : IHttpRequestWrapper
{
    public Uri? RequestUri()
    {
        var request = httpContextAccessor.HttpContext?.Request;

        if (request == null) return null;

        return new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");
    }

    public string Header(string name)
    {
        if (httpContextAccessor.HttpContext == null) return string.Empty;

        return httpContextAccessor.HttpContext.Request.Headers.TryGetValue(name, out var value)
            ? value.ToString()
            : string.Empty;
    }

    public string Query(string name)
    {
        if (httpContextAccessor.HttpContext == null) return string.Empty;

        return httpContextAccessor.HttpContext.Request.Query[name].ToString();
    }

    public string RootTraceId() => System.Diagnostics.Activity.Current?.RootId ?? string.Empty;
    public string TraceId() => System.Diagnostics.Activity.Current?.Id ?? string.Empty;

    public bool IsAuthenticated() => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    public string? UserId() => httpContextAccessor.HttpContext?.User.Identity?.Name;
}