using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

internal sealed class DefaultScreenSectionsProvider<TRequest>(
        IEnumerable<IScreenSectionProvider<TRequest>> sectionProviders,
        IEnumerable<IScreenMetaDataProvider<TRequest>> metaDataProviders,
        ISectionFeatureFlag sectionFeatureFlag)
    : IScreenSectionsProvider<TRequest>
{
    public async Task<MaySucceed<ScreenSectionResponseDto>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var tasks = new List<Task<MaySucceed<(ScreenSection? Section, IEnumerable<IMetaData> MetaData)>>>();
        var requestData = context.RequestData();
        var isSectionOnlyRequest = requestData.IsSectionOnlyRequest();
        
        foreach (var provider in sectionProviders)
        {
            if (isSectionOnlyRequest)
            {
                if(requestData.IsSectionRequested(provider.ForSection) == false) continue;
            }

            if (provider.IsApplicable(context, request) == false) continue;
            
            tasks.Add(Execute(provider, context, request, ct));
        }

        foreach (var provider in metaDataProviders)
        {
            if (isSectionOnlyRequest)
            {
                if (string.IsNullOrWhiteSpace(provider.ForSection) == false)
                {
                    if (requestData.IsSectionRequested(provider.ForSection) == false) continue;
                }
            }

            if (provider.IsApplicable(context, request) == false) continue;
            
            tasks.Add(Execute(provider, context, request, ct));
        }

        await Task.WhenAll(tasks);

        var sections = new List<ScreenSection>();
        var metaData = new List<IMetaData>();

        foreach (var task in tasks)
        {
            var rsp = task.Result;

            if (rsp.IsFailed) return rsp.Failure;

            if (rsp.Value.Section != null)
            {
                sections.Add(rsp.Value.Section);
            }

            metaData.AddRange(rsp.Value.MetaData);
        }

        return new ScreenSectionResponseDto
        {
            Sections = sections,
            MetaData = metaData
        };
    }

    private static (ScreenSection? Section, IEnumerable<IMetaData> MetaData) EmptyTaskResponse =
        (null, Enumerable.Empty<IMetaData>());
    
    private async Task<MaySucceed<(ScreenSection? Section, IEnumerable<IMetaData> MetaData)>> Execute(
        IScreenSectionProvider<TRequest> provider,
        IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        if (await sectionFeatureFlag.IsDisabled(provider.ForSection)) 
            return (null, Enumerable.Empty<IMetaData>());

        var rsp = await provider.Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        if (rsp.Value is EmptyElement) return EmptyTaskResponse;
        
        return (new ScreenSection
        {
            Element = rsp.Value,
            Name = provider.ForSection
        }, Enumerable.Empty<IMetaData>());
    }
    
    private async Task<MaySucceed<(ScreenSection? Section, IEnumerable<IMetaData> MetaData)>> Execute(
        IScreenMetaDataProvider<TRequest> provider,
        IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(provider.ForSection) == false)
        {
            if (await sectionFeatureFlag.IsDisabled(provider.ForSection) == false)
                return (null, Enumerable.Empty<IMetaData>());
        }

        var rsp = await provider.Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return (null, rsp.Value);
    }

    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}