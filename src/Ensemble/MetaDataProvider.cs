using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

public abstract class MetaDataProvider<TRequest> : IScreenSectionsProvider<TRequest>
{
    protected abstract Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    async Task<MaySucceed<ScreenSectionResponseDto>> IScreenSectionsProvider<TRequest>.Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var elementRsp = await Get(context, request, ct);

        if (elementRsp.IsFailed) return elementRsp.Failure;

        if (elementRsp.Value == null)
            return new ScreenSectionResponseDto
            {
                Sections = Enumerable.Empty<ScreenSection>(),
                MetaData = Enumerable.Empty<IMetaData>()
            };

        return new ScreenSectionResponseDto
        {
            Sections = Enumerable.Empty<ScreenSection>(),
            MetaData = elementRsp.Value
        };
    }
}