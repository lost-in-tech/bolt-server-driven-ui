using Bolt.MaySucceed;
using Bolt.Sdui.Core;

namespace Ensemble.Core;

public interface IScreenComposer<in TRequest>
{
    Task<MaySucceed<Screen>> Compose(TRequest request, CancellationToken ct);
}

public record Screen
{
    public required Dictionary<string, ScreenLayout> Layouts { get; init; }
    public required IEnumerable<ScreenSection> Sections { get; init; }
    public required IMetaData[] MetaData { get; init; }
}

public record ScreenLayout
{
    public string? VersionId { get; init; }
    public required IElement Element { get; init; }
}

public record ScreenSection
{
    public required string Name { get; init; }
    public required IElement Element { get; init; }
}