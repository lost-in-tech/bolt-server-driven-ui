using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface IExternalScreenProvider
{
    Task<MaySucceed<Screen>> Get(IRequestContextReader context, ExternalScreenRequest request, CancellationToken ct);
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