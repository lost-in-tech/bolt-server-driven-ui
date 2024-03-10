namespace Ensemble.Extensions.Web.LayoutProviders;

public record RazorLayoutProviderSettings
{
    public string? ViewFolder { get; init; } = "Views";
    public string? RootFolder { get; init; } = "Features";
    public string? Version { get; init; }
}