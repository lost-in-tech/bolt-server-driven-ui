using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    SectionInfo[] IScreenSectionProvider<TRequest>.ForSections(IRequestContextReader context, TRequest request)
        => [ForSection];

    public abstract Task<MaySucceed<ScreenSectionResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    protected abstract SectionInfo ForSection { get; }

    protected MaySucceed<ScreenSectionResponse> ScreenSection(IElement element) =>
        new(new ScreenSectionResponse
        {
            Sections = [new ScreenSection
            {
                Element = element,
                Name = ForSection.Name
            }],
            MetaData = Enumerable.Empty<IMetaData>()
        });
    
    protected MaySucceed<ScreenSectionResponse> ScreenSection(IMetaData metaData) =>
        new(new ScreenSectionResponse
        {
            Sections = [],
            MetaData = [metaData]
        });
    
    protected MaySucceed<ScreenSectionResponse> ScreenSection(IEnumerable<IMetaData> metaData) =>
        new(new ScreenSectionResponse
        {
            Sections = [],
            MetaData = metaData
        });
    
    protected MaySucceed<ScreenSectionResponse> ScreenSection(params IMetaData[] metaData) =>
        new(new ScreenSectionResponse
        {
            Sections = [],
            MetaData = metaData
        });
}