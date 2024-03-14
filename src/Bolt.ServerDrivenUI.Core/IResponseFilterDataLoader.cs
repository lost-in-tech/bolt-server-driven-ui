using Bolt.Endeavor;
namespace Bolt.ServerDrivenUI.Core;

public interface IResponseFilterDataLoader<in TRequest>
{
    Task<MaySucceed<ResponseFilterData>> Load(IRequestContext context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public record ResponseFilterData
{
    public required string Key { get; init; }
    public required object Value { get; init; }
}