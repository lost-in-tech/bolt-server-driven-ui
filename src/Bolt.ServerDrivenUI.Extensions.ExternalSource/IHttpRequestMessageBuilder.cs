using System.Text;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IHttpRequestMessageBuilder
{
    HttpRequestMessage Build(RequestMessageBuilderInput input);
}


public record RequestMessageBuilderInput
{
    public required HttpMethod Method { get; init; } = HttpMethod.Get;
    public required string Path { get; init; }
    public IEnumerable<(string Key, string? Value)>? QueryStrings { get; init; } 
    public IEnumerable<(string Key, string? Value)>? Headers { get; init; }
}