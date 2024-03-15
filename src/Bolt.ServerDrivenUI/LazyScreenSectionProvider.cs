using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class LazyScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    string[] IScreenSectionProvider<TRequest>.ForSections => new[]{ ForSection };
    
    public virtual bool IsLazy(IRequestContextReader context, TRequest request) => true;
    
    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        var isLazy = IsLazy(context, request);
        var requestMode = context.RequestData().Mode;
        var isLazyRequest = requestMode == RequestMode.LazySections
                            && isLazy; 
        
        var rsp = isLazyRequest
            ? await GetLazy(context, request, ct)
            : await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Elements = rsp.Value is EmptyElement ? Enumerable.Empty<ScreenSection>() : new [] { 
                new ScreenSection
                {
                    Element = rsp.Value,
                    IsLazy = isLazy && requestMode != RequestMode.LazySections ? true : null,
                    Name = ForSection
                }
            },
            MetaData = Enumerable.Empty<IMetaData>()
        };
    }
    
    protected abstract string ForSection { get; }
    
    protected abstract Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);
    
    protected abstract Task<MaySucceed<IElement>> GetLazy(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}