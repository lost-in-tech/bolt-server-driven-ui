using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Core;

public interface IScreenBuildingBlocksProvider<in TRequest>
{
    Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Get(
        IRequestContextReader context, 
        TRequest request, 
        CancellationToken ct);
    
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenBuildingBlocksProvider<TRequest> : IScreenBuildingBlocksProvider<TRequest>
{
    public abstract Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Get(
        IRequestContextReader context, 
        TRequest request,
        CancellationToken ct);
    
    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

public record ScreenBuildingBlocksResponseDto
{
    public required IEnumerable<ScreenSection> Sections { get; init; }
    public required IEnumerable<IMetaData> MetaData { get; init; }
}
