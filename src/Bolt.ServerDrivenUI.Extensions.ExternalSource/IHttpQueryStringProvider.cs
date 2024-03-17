namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpQueryStringProvider
{
    public IEnumerable<(string Key, string? Value)> Get();
}