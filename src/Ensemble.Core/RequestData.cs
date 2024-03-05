namespace Ensemble.Core;

public record RequestData
{
    public required string RootApp { get; init; }
    public required string RootId { get; init; }
    public required string Id { get; init; }
    public required string App { get; init; }
    public Platform? Platform { get; init; }
    public Device? Device { get; init; }
    public string? UserAgent { get; init; }
 
    public string[]? SectionNames { get; init; }
    
    /// <summary>
    /// Layout version client has in cache. Layout content will return only if LayoutVersion is empty or doesn't match
    /// with current version server has 
    /// </summary>
    public string? LayoutVersionId { get; init; }
}

public enum Platform
{
    Unknown,
    Windows,
    Linux,
    Mac,
    Android,
    Ios,
}

public enum Device
{
    Desktop,
    Mobile,
    Tablet
}