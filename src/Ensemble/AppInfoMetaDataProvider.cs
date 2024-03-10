using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

internal sealed class AppInfoMetaDataProvider<TRequest>(IAppInfoProvider appInfoProvider) : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IMetaData[]>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var appInfo = appInfoProvider.Get();

        return Task.FromResult<MaySucceed<IMetaData[]>>(new IMetaData[]
        {
            new AppInfoMetaData
            {
                Version = appInfo.Version,
                AppName = appInfo.Name
            }
        });
    }
}

public record AppInfoMetaData : IMetaData
{
    public required string AppName { get; init; }
    public required Version Version { get; init; }
}