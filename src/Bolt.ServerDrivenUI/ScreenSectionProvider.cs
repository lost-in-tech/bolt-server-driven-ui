using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    SectionInfo[] IScreenSectionProvider<TRequest>.ForSections(IRequestContextReader context, TRequest request)
        => [ForSection];

    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request, CancellationToken ct)
    {
        var rsp = await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse()
        {
            Sections = rsp.Value.Element == null
                ? []
                :
                [
                    new ScreenSection
                    {
                        Element = rsp.Value.Element,
                        Name = ForSection.Name
                    }
                ],
            MetaData = rsp.Value.MetaData ?? Enumerable.Empty<IMetaData>()
        };
    }

    protected abstract Task<MaySucceed<ScreenElement>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    protected abstract SectionInfo ForSection { get; }
}

public record ScreenElement
{
    private ScreenElement(IElement? element, IEnumerable<IMetaData>? metaData)
    {
        Element = element;
        MetaData = metaData;
    }
    
    public IElement? Element { get; set; }
    public IEnumerable<IMetaData>? MetaData { get; set; }


    private static readonly ScreenElement Empty = new ScreenElement(null, null);
    public static ScreenElement None => Empty;
    public static ScreenElement New(IElement? element) => new(element, Enumerable.Empty<IMetaData>());
    public static ScreenElement New(IMetaData? metaData) => new(null, [metaData]);
    public static ScreenElement New(params IMetaData[] metaData) => new(null, metaData);
    
    public static ScreenElement New(IElement? element, params IMetaData[] metaData) => new(element, metaData);
}