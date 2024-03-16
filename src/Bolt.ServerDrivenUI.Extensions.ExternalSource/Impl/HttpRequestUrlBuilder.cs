using System.Text;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;

internal sealed class HttpRequestUrlBuilder(
        IEnumerable<IHttpQueryStringProvider> queryStringProviders) 
    : IHttpRequestUrlBuilder
{
    private const char CharQuestion = '?';
    private const char CharAmpersand = '&';
    private const char CharEqual = '=';
    
    public string Build(string url, IEnumerable<(string Key, string? Value)>? queryStrings = null)
    {
        var urlParts = url.Split(CharQuestion);

        var sb = new StringBuilder(urlParts[0]);
        
        return AppendQueryString(sb, urlParts.Length > 1 ? urlParts[1] : string.Empty, queryStrings);
    }
    
    private string AppendQueryString(StringBuilder sb, string existing, IEnumerable<(string Key, string? Value)>? queryStrings)
    {
        sb.Append(CharQuestion);

        var hasExisting = string.IsNullOrWhiteSpace(existing) == false;
        if (hasExisting)
        {
            sb.Append(existing);
        }

        var total = 0;

        if (queryStrings != null)
        {
            foreach (var item in queryStrings)
            {
                if (item.Value != null)
                {
                    if (total > 0)
                    {
                        sb.Append(CharAmpersand);
                    }
                    else if (hasExisting == false)
                    {
                        sb.Append(CharAmpersand);
                    }
                    
                    sb.Append(item.Key).Append(CharEqual).Append(Uri.EscapeDataString(item.Value)).Append(CharAmpersand);
                    
                    total++;
                }
            }
        }

        foreach (var queryStringProvider in queryStringProviders)
        {
            var keyValues = queryStringProvider.Get();

            foreach (var item in keyValues)
            {
                if (item.Value != null)
                {
                    if (total > 0)
                    {
                        sb.Append(CharAmpersand);
                    }
                    else if (hasExisting)
                    {
                        sb.Append(CharAmpersand);
                    }

                    sb.Append(item.Key).Append(CharEqual).Append(Uri.EscapeDataString(item.Value));
                    
                    total++;
                }
            }
        }

        return sb.ToString();
    }
}