using System.Reflection.Emit;

namespace Ensemble.Core;

public record RequestData
{
    public required string RootApp { get; init; }
    public required string CorrelationId { get; init; }
    public required string App { get; init; }
    public Platform? Platform { get; init; }
    public Device? Device { get; init; }
    public string? UserAgent { get; init; }
    public RequestScreenSize? ScreenSize { get; init; }
    
    public required string Tenant { get; init; } = string.Empty;

    public string[] SectionNames { get; init; } = Array.Empty<string>();
    
    /// <summary>
    /// Layout version client has in cache. Layout content will return only if LayoutVersion is empty or doesn't match
    /// with current version server has 
    /// </summary>
    public string? LayoutVersionId { get; init; }

    public bool IsSectionOnlyRequest() => SectionNames.Length > 0;

    public bool IsSectionRequested(string sectionName) => SectionNames.Any(section =>
        string.Equals(section, sectionName, StringComparison.OrdinalIgnoreCase));
    
    public string? UserId { get; init; }
    public bool IsAuthenticated { get; init; }
}


public enum RequestScreenSize
{
    Wide,
    Compact
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