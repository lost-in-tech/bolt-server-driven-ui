using System.Reflection;
using Ensemble.Core;

namespace Ensemble;

internal sealed class AppInfoProvider : IAppInfoProvider
{
    private static readonly Lazy<AppInfo> _instance = new Lazy<AppInfo>(() => new AppInfo
    {
        Name = AppDomain.CurrentDomain.FriendlyName.Replace(".", "-").ToLowerInvariant(),
        Version = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(1, 0, 0)
    });

    public AppInfo Get() => _instance.Value;
}