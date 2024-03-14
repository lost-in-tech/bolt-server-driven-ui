using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    string[] IScreenSectionProvider<TRequest>.ForSections => new[]{ ForSection };
    
    bool IScreenSectionProvider<TRequest>.IsLazy(IRequestContextReader context, TRequest request, CancellationToken ct) => false;
    
    async Task<Bolt.Endeavor.MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        var rsp = await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Elements = rsp.Value is EmptyElement ? Enumerable.Empty<ScreenSection>() : new [] { 
                new ScreenSection
                {
                    Element = rsp.Value,
                    Name = ForSection
                }
            },
            MetaData = Enumerable.Empty<IMetaData>()
        };
    }
    
    public abstract string ForSection { get; }
    
    public abstract Task<Bolt.Endeavor.MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

}

public static class Element
{
    private static EmptyElement _none = new EmptyElement();
    public static EmptyElement None => _none;

    public static Task<MaySucceed<IElement>> ToMaySucceedTask(this IElement element) =>
        Task.FromResult(MaySucceed<IElement>.Ok(element));
    public static MaySucceed<IElement> ToMaySucceed(this IElement element) =>
        MaySucceed<IElement>.Ok(element);
}