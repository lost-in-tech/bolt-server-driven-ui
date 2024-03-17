using System.Text;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpRequestUrlBuilder(
        IEnumerable<IHttpQueryStringProvider> queryStringProviders) 
    : IHttpRequestUrlBuilder
{
    public string Build(string url, IEnumerable<(string Key, string? Value)>? queryStrings = null)
    {
        return UrlHelper.AppendQueryString(url, GetAllQueryStrings(queryStrings));
    }

    private IEnumerable<(string Key, string? Value)>? GetAllQueryStrings(
        IEnumerable<(string Key, string? Value)>? queryStrings = null)
    {
        foreach (var queryStringProvider in queryStringProviders)
        {
            var keyValues = queryStringProvider.Get();

            foreach (var item in keyValues)
            {
                yield return item;
            }
        }

        if (queryStrings == null) yield break;

        foreach (var item in queryStrings)
        {
            yield return item;
        }
    }
}


internal static class UrlHelper
{
    private const char CharQuestion = '?';
    private const char CharAmpersand = '&';
    private const char CharEqual = '=';
    public static string AppendQueryString(string path, IEnumerable<(string Key, string? Value)>? queryStrings)
    {
        if (queryStrings == null) return path;
        
        var urlParts = path.Split(CharQuestion);

        return AppendQueryString(urlParts, queryStrings);
    }
    
    private static string AppendQueryString(string[] urlParts, IEnumerable<(string Key, string? Value)> queryStrings)
    {
        var path = urlParts[0];
        var queryParts = urlParts.Length > 1 ? urlParts[1] : string.Empty;

        StringBuilder? sb = null;
        
        var total = 0;
        var hasQueryParts = !string.IsNullOrWhiteSpace(queryParts);
        
        foreach (var item in queryStrings)
        {
            if (item.Value != null)
            {
                sb ??= new StringBuilder(path);
                
                if (total > 0)
                {
                    sb.Append(CharAmpersand);
                }
                else
                {
                    if (hasQueryParts)
                    {
                        sb.Append(CharQuestion).Append(queryParts).Append(CharAmpersand);
                    }
                    else
                    {
                        sb.Append(CharQuestion);
                    }
                }
                    
                sb.Append(item.Key).Append(CharEqual).Append(Uri.EscapeDataString(item.Value));
                    
                total++;
            }
        }

        return sb == null ? path : sb.ToString();
    }
}