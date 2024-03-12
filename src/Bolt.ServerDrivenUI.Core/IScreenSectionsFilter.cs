using Bolt.MaySucceed;

namespace Bolt.ServerDrivenUI.Core;

public interface IScreenSectionsFilter<TRequest>
{
    Task<MaySucceed<TRequest>> OnRequest(IRequestContextWriter context, TRequest request, CancellationToken ct);
    Task<MaySucceed<ScreenSectionResponseDto>> OnResponse(IRequestContextReader context, 
        TRequest request, 
        ScreenSectionResponseDto response, 
        CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
    int Priority { get; }
}


public abstract class ScreenSectionsFilter<TRequest> : IScreenSectionsFilter<TRequest>
{
    public virtual Task<MaySucceed<TRequest>> OnRequest(
        IRequestContextWriter context, 
        TRequest request,
        CancellationToken ct) 
        => Task.FromResult<MaySucceed<TRequest>>(request);

    public virtual Task<MaySucceed<ScreenSectionResponseDto>> OnResponse(
        IRequestContextReader context, 
        TRequest request,
        ScreenSectionResponseDto response, 
        CancellationToken ct)
        => Task.FromResult(new MaySucceed<ScreenSectionResponseDto>(response));

    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
    public virtual int Priority => 0;
}