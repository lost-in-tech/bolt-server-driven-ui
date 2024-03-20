using System.Security.Cryptography;
using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public abstract class ExternalSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    public abstract string[] ForSections { get; }
    
    async Task<MaySucceed<ScreenSectionResponse>> IScreenSectionProvider<TRequest>.Get(
        IRequestContextReader context, 
        TRequest request, 
        CancellationToken ct)
    {
        var rsp = await Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return new ScreenSectionResponse
        {
            Sections = rsp.Value.Sections,
            MetaData = rsp.Value.MetaData ?? Enumerable.Empty<IMetaData>()
        };
    }

    protected abstract Task<MaySucceed<Screen>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}