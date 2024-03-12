using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IScreenMetaDataProvider<in TRequest>
{
    string? ForSection { get; }
    Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenMetaDataProvider<TRequest> : IScreenMetaDataProvider<TRequest>
{
    public virtual string? ForSection => null;

    public abstract Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    protected MaySucceed<IEnumerable<IMetaData>> ToResponse(IMetaData metaData) => MaySucceed<IEnumerable<IMetaData>>.Ok(new[] { metaData });
    protected MaySucceed<IEnumerable<IMetaData>> ToResponse(IEnumerable<IMetaData> metaData) => MaySucceed<IEnumerable<IMetaData>>.Ok(metaData);
    
    protected Task<MaySucceed<IEnumerable<IMetaData>>> ToResponseTask(IMetaData metaData) => Task.FromResult(ToResponse(metaData));
    protected Task<MaySucceed<IEnumerable<IMetaData>>> ToResponseTask(IEnumerable<IMetaData> metaData) => Task.FromResult(ToResponse(metaData));
    
    

    protected Task<MaySucceed<IEnumerable<IMetaData>>> NoneAsTask() => ToResponseTask(Enumerable.Empty<IMetaData>());
    protected MaySucceed<IEnumerable<IMetaData>> None() => MaySucceed<IEnumerable<IMetaData>>.Ok(Enumerable.Empty<IMetaData>());
}