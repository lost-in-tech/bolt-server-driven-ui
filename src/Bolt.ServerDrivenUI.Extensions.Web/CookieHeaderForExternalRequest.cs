using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;
using Microsoft.AspNetCore.Http;

namespace Bolt.ServerDrivenUI.Extensions.Web;

internal sealed class CookieHeaderForExternalRequest(
    IHttpRequestWrapper requestWrapper) 
    : IHttpRequestHeadersProvider
{
    public IEnumerable<(string Key, string Value)> Get(IRequestContextReader context)
    {
        var cookies = requestWrapper.AllCookies();

        var cookieValue = BuildCookieValue(cookies.ToArray());
        
        if(string.IsNullOrWhiteSpace(cookieValue)) yield break;
        
        yield return ("Cookie", cookieValue);
    }

    private string BuildCookieValue((string Key, string Value)[] cookies)
    {
        if (cookies.Length == 0) return string.Empty;
        
        var sb = new StringBuilder();
        
        foreach (var cookie in cookies)
        {
            sb.Append($"{cookie.Key}={cookie.Value}; ");
        }

        var result = sb.ToString();
        
        return result.Substring(0, result.Length -2);
    }
}