using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

internal sealed class AppInfoMetaDataProvider<TRequest>(IAppInfoProvider appInfoProvider) : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var appInfo = appInfoProvider.Get();

        return ToResponseTask(new AppInfoMetaData
        {
            Version = appInfo.Version,
            AppName = appInfo.Name
        });
    }
}

public record AppInfoMetaData : IMetaData
{
    public required string AppName { get; init; }
    public required Version Version { get; init; }
}