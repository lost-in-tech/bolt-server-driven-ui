namespace Ensemble.Core;

public interface IRequestKeyNamesProvider
{
    public RequestKeyNames Get();
}

public record RequestKeyNames
{
    public required string App { get; init; }
    public required string Id { get; init; }
    public required string RootApp { get; init; }
    public required string RootId { get; init; }
    public required string Device { get; init; }
    public required string Platform { get; init; }
    public required string LayoutVersionId { get; init; }
    public required string SectionNames { get; init; }
    public required string ScreenSize { get; init; }
}