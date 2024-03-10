using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

public interface IScreenMetaDataProvider<in TRequest>
{
    string? ForSection { get; }
    Task<MaySucceed<IMetaData[]>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenMetaDataProvider<TRequest> : IScreenMetaDataProvider<TRequest>
{
    public virtual string? ForSection => null;

    public abstract Task<MaySucceed<IMetaData[]>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}