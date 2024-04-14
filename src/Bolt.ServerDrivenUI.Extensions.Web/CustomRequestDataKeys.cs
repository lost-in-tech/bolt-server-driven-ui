namespace Bolt.ServerDrivenUI.Extensions.Web;

public class CustomRequestDataKeys
{
    public string? Mode { get; init; }
    public string? App { get; init; }
    public string? CorrelationId { get; init; }
    public string? RootApp { get; init; }
    public string? RootId { get; init; }
    public string? Device { get; init; }
    public string? Platform { get; init; }
    public string? LayoutVersionId { get; init; }
    public string? SectionNames { get; init; }
    public string? ScreenSize { get; init; }
    public string? Tenant { get; init; }
    public string? RootRequestUri { get; init; }
}