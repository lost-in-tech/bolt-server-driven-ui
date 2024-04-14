using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public abstract class ScreenMetaDataProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    SectionInfo[] IScreenSectionProvider<TRequest>.ForSections(IRequestContextReader context, TRequest request) 
        => ForSection == null ? [] : [ ForSection.Value ];
    
    async Task<Bolt.Endeavor.MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        var rsp = await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Sections = Enumerable.Empty<ScreenSection>(),
            MetaData = rsp.Value
        };
    }

    protected virtual SectionInfo? ForSection => null;

    protected abstract Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

public static class MetaData
{
    public static MaySucceed<IEnumerable<IMetaData>> ToMaySucceed(this IMetaData metaData) => new(new[] { metaData });
    public static MaySucceed<IEnumerable<IMetaData>> ToMaySucceed(this IEnumerable<IMetaData> metaData) => new(metaData);
    public static Task<MaySucceed<IEnumerable<IMetaData>>> ToMaySucceedTask(this IMetaData metaData) => Task.FromResult(ToMaySucceed(metaData));
    public static Task<MaySucceed<IEnumerable<IMetaData>>> ToMaySucceedTask(this IEnumerable<IMetaData> metaData) => Task.FromResult(ToMaySucceed(metaData));
}