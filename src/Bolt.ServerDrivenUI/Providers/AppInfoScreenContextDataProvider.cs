using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class AppInfoScreenContextDataProvider(
        IAppInfoProvider appInfoProvider) 
    : IScreenContextDataProvider
{
    public IEnumerable<(string Key, object? Value)> Get(IRequestContextReader context)
    {
        var appInfo = appInfoProvider.Get();
        yield return ("app", new
        {
            Name = appInfo.Name,
            BaseUrl = appInfo.BaseUrl,
            Version = appInfo.Version
        });
    }
}