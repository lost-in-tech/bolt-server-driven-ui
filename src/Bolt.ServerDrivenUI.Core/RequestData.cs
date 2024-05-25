namespace Bolt.ServerDrivenUI.Core;

public record RequestData
{
    public RequestMode Mode { get; init; } = RequestMode.Default;
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
    public Dictionary<string,string>? LayoutVersions { get; init; }
    
    public string? UserId { get; init; }
    public bool IsAuthenticated { get; init; }
    
    public Uri? RootRequestUri { get; init; }
    
    public string? AuthToken { get; init; }
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

public enum RequestMode
{
    Default = 0,
    Sections = 10,
    LazySections = 20
}

public enum Device
{
    Desktop,
    Mobile,
    Tablet
}