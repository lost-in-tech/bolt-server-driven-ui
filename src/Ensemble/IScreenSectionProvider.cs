using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

public interface IScreenSectionProvider<in TRequest>
{
    string ForSection { get; }
    Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    public abstract string ForSection { get; }

    public abstract Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}