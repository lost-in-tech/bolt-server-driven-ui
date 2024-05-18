using System.Reflection;
using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web;

internal sealed class AppInfoProvider(IOptions<AppInfoSettings> options) : IAppInfoProvider
{
    private readonly Lazy<AppInfo> _instance = new Lazy<AppInfo>(() => new AppInfo
    {
        Name = options.Value.Name ?? AppDomain.CurrentDomain.FriendlyName.ToLowerInvariant().Replace(".", "-"),
        BaseUrl = options.Value.BaseUrl, 
        Version = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(1, 0, 0)
    });

    public AppInfo Get() => _instance.Value;
}

public record AppInfoSettings
{
    public string? Name { get; init; }
    public string? BaseUrl { get; init; }
}