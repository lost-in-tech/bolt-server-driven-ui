using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class LazyScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    SectionInfo[] IScreenSectionProvider<TRequest>.ForSections(IRequestContextReader context, TRequest request)
        => [new SectionInfo
        {
            Name = ForSection,
            Scope = SectionScope.Default
        },
        new SectionInfo
        {
            Name = ForSection,
            Scope = SectionScope.Lazy
        }];

    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var mode = context.RequestData().Mode;
        
        var elmRsp = mode == RequestMode.LazySections 
            ? await Lazy(context, request)
            : await Default(context, request);

        if (elmRsp.IsFailed) return elmRsp.Failure;

        return new ScreenSectionResponse
        {
            Sections = elmRsp.Value.Element == null ? Enumerable.Empty<ScreenSection>() :
            [
                new ScreenSection
                {
                    Element = elmRsp.Value.Element,
                    Name = ForSection
                }
            ],
            MetaData = elmRsp.Value.MetaData ?? Enumerable.Empty<IMetaData>()
        };
    }

    /// <summary>
    /// Provide the element we need to display by default.
    /// For example, you might want to display as element that display loading screen
    /// </summary>
    /// <param name="context"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    protected abstract Task<MaySucceed<ScreenElement>> Default(IRequestContextReader context, TRequest request);
    
    /// <summary>
    /// Actual element that gonna render after page load
    /// </summary>
    /// <param name="context"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    protected abstract Task<MaySucceed<ScreenElement>> Lazy(IRequestContextReader context, TRequest request);
    
    protected abstract string ForSection { get; }

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}