using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IExternalScreenRequestProvider<in TRequest>
{
    IEnumerable<ExternalScreenRequest> Get(IRequestContextReader context, TRequest request);
}

public record ExternalScreenRequest
{
    public required string ServiceName { get; init; }
    public required string Path { get; init; }
    public HttpMethod Method { get; init; } = HttpMethod.Get;
    public IEnumerable<(string Key, string? Value)>? QueryStrings { get; init; }
    public IEnumerable<(string Key, string? Value)>? Headers { get; init; }
    public object? Content { get; init; }
    public required SectionInfo[] ForSections { get; init; }
}