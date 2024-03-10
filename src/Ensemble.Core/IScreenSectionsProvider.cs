using Bolt.MaySucceed;
using Bolt.Sdui.Core;

namespace Ensemble.Core;

public interface IScreenSectionsProvider<in TRequest>
{
    Task<MaySucceed<ScreenSectionResponseDto>> Get(
        IRequestContextReader context, 
        TRequest request, 
        CancellationToken ct);
    
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenSectionsProvider<TRequest> : IScreenSectionsProvider<TRequest>
{
    public abstract Task<MaySucceed<ScreenSectionResponseDto>> Get(
        IRequestContextReader context, 
        TRequest request,
        CancellationToken ct);

    public abstract string Name { get; }
    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

public record ScreenSectionResponseDto
{
    public required IEnumerable<ScreenSection> Sections { get; init; }
    public required IEnumerable<IMetaData> MetaData { get; init; }
}
