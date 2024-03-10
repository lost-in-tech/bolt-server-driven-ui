using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

public abstract class SectionElementProvider<TRequest> : IScreenSectionsProvider<TRequest>
{
    protected abstract string Name { get; }
    protected abstract Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    protected virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    bool IScreenSectionsProvider<TRequest>.IsApplicable(IRequestContextReader context, TRequest request)
    {
        var requestData = context.RequestData();
        
        if (!requestData.IsSectionOnlyRequest()) return true;

        return requestData.IsSectionRequested(Name);
    }
    
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
            Sections = new []{ new ScreenSection { Name  = Name, Element = elementRsp.Value } },
            MetaData = Enumerable.Empty<IMetaData>()
        };
    }
}