using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    SectionInfo[] IScreenSectionProvider<TRequest>.ForSections(IRequestContextReader context, TRequest request) =>
        [ForSection];
    
    async Task<Bolt.Endeavor.MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        var rsp = await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Sections = rsp.Value is EmptyElement ? Enumerable.Empty<ScreenSection>() : new [] { 
                new ScreenSection
                {
                    Element = rsp.Value,
                    Name = ForSection.Name
                }
            },
            MetaData = Enumerable.Empty<IMetaData>()
        };
    }
    
    protected abstract SectionInfo ForSection { get; }
    
    protected abstract Task<Bolt.Endeavor.MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

}

public static class Element
{
    public static IElement None { get; } = EmptyElement.Instance;

    public static Task<MaySucceed<IElement>> ToMaySucceedTask(this IElement element) =>
        Task.FromResult(MaySucceed<IElement>.Ok(element));
    public static MaySucceed<IElement> ToMaySucceed(this IElement element) =>
        MaySucceed<IElement>.Ok(element);
}