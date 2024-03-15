using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class AppInfoScreenContextDataProvider(
        IAppInfoProvider appInfoProvider) 
    : IScreenContextDataProvider
{
    public IEnumerable<(string Key, object? Value)> Get(IRequestContextReader context)
    {
        var appInfo = appInfoProvider.Get();
        yield return ("App", new
        {
            Name = appInfo.Name,
            Version = appInfo.Version
        });
    }
}