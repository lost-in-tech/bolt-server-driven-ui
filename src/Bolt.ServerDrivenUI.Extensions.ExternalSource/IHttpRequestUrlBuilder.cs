namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpRequestUrlBuilder
{
    public string Build(string url,  IEnumerable<(string Key, string? Value)>? queryStrings = null);
}