using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    string[] IScreenSectionProvider<TRequest>.ForSections => new[]{ ForSection };

    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
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
    
    public abstract Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
    
    protected Task<MaySucceed<IElement>> ToResponseTask(IElement element) =>
        Task.FromResult(ToResponse(element));
    protected MaySucceed<IElement> ToResponse(IElement element) =>
        MaySucceed<IElement>.Ok(element);

    private static readonly IElement EmptyElement = new EmptyElement();
    protected Task<MaySucceed<IElement>> NoneAsTask() => ToResponseTask(EmptyElement);
    protected MaySucceed<IElement> None() => ToResponse(EmptyElement);
}