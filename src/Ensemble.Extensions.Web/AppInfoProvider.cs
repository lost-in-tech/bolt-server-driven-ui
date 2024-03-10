using System.Reflection;
using Ensemble.Core;
using Microsoft.Extensions.Options;

namespace Ensemble.Extensions.Web;

internal sealed class AppInfoProvider(IOptions<AppInfoSettings> options) : IAppInfoProvider
{
    private readonly Lazy<AppInfo> _instance = new Lazy<AppInfo>(() => new AppInfo
    {
        Name = options.Value?.Name ?? AppDomain.CurrentDomain.FriendlyName.ToLowerInvariant().Replace(".", "-"),
        Version = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(1, 0, 0)
    });

    public AppInfo Get() => _instance.Value;
}



public record AppInfoSettings
{
    public string? Name { get; init; }
}