namespace Bolt.ServerDrivenUI.Core;

public interface IAppInfoProvider
{
    public AppInfo Get();
}

public record AppInfo
{
    public required string Name { get; init; }
    public string? BaseUrl { get; init; }
    public required Version Version { get; init; }
}