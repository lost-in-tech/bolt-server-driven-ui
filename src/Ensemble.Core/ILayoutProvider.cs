using Bolt.MaySucceed;
using Bolt.Sdui.Core;

namespace Ensemble.Core;

public interface ILayoutProvider<in TRequest>
{
    Task<MaySucceed<LayoutResponse>> Get(
        IRequestContextReader context, 
        TRequest request, 
        CancellationToken ct);
    
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public record LayoutResponse
{
    public required string Name { get; init; }
    public string? VersionId { get; init; }
    public required IElement Element { get; init; }
}

public abstract class LayoutProvider<TRequest> : ILayoutProvider<TRequest>
{
    public abstract Task<MaySucceed<LayoutResponse>> Get(
        IRequestContextReader context, 
        TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}