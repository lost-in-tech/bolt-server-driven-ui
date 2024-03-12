namespace Bolt.ServerDrivenUI.Core;

public interface IRequestContextDataLoader<in TRequest>
{
    Task Load(IRequestContext context, TRequest request, CancellationToken ct);
    bool IsApplicable(TRequest request);
}

public abstract class RequestContextDataLoader<TRequest> : IRequestContextDataLoader<TRequest>
{
    public abstract Task Load(IRequestContext context, TRequest request, CancellationToken ct);

    public virtual bool IsApplicable(TRequest request) => true;
}