using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI;

public abstract class LazyScreenMetaDataProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    string[] IScreenSectionProvider<TRequest>.ForSections => string.IsNullOrWhiteSpace(ForSection) ? Array.Empty<string>() : new[]{ ForSection };
    
    public virtual bool IsLazy(IRequestContextReader context, TRequest request, CancellationToken ct) => true;
    
    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        var rsp = context.RequestData().IsSectionOnlyRequest() 
            ? await GetLazy(context, request, ct)
            : await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Elements = Enumerable.Empty<ScreenSection>(),
            MetaData = rsp.Value
        };
    }

    protected virtual string ForSection => string.Empty;

    protected abstract Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);
    
    protected abstract Task<MaySucceed<IEnumerable<IMetaData>>> GetLazy(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}