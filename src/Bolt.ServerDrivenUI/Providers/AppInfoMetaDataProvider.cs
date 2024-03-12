using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class AppInfoMetaDataProvider<TRequest>(IAppInfoProvider appInfoProvider) : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var appInfo = appInfoProvider.Get();

        return new AppInfoMetaData
        {
            Version = appInfo.Version,
            AppName = appInfo.Name
        }.ToMaySucceedTask();
    }
}

public record AppInfoMetaData : IMetaData
{
    public required string AppName { get; init; }
    public required Version Version { get; init; }
}