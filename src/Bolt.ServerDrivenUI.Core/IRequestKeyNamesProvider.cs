namespace Bolt.ServerDrivenUI.Core;

public interface IRequestKeyNamesProvider
{
    public RequestKeyNames Get();
}

public record RequestKeyNames
{
    public required string Mode { get; init; }
    public required string App { get; init; }
    public required string CorrelationId { get; init; }
    public required string RootApp { get; init; }
    public required string Device { get; init; }
    public required string Platform { get; init; }
    public required string LayoutVersionId { get; init; }
    public required string SectionNames { get; init; }
    public required string ScreenSize { get; init; }
    public required string Tenant { get; init; }
    public required string TenantQs { get; init; }
    public required string RootRequestUri { get; init; }
    
    public required string AuthToken { get; init; }
}