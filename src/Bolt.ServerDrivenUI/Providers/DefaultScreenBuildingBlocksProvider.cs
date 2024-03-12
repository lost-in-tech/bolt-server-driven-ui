using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class DefaultScreenBuildingBlocksProvider<TRequest>(
        IEnumerable<IScreenSectionProvider<TRequest>> sectionProviders,
        ISectionFeatureFlag sectionFeatureFlag)
    : IScreenBuildingBlocksProvider<TRequest>
{
    public async Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var tasks = new List<Task<MaySucceed<(IEnumerable<ScreenSection> Sections, IEnumerable<IMetaData> MetaData)>>>();
        var requestData = context.RequestData();
        var isSectionOnlyRequest = requestData.IsSectionOnlyRequest();
        
        foreach (var provider in sectionProviders)
        {
            if (isSectionOnlyRequest)
            {
                if (provider.ForSections.Length > 0)
                {
                    if (requestData.IsSectionRequested(provider.ForSections) == false) continue;
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

            sections.AddRange(rsp.Value.Sections);

            metaData.AddRange(rsp.Value.MetaData);
        }

        return new ScreenBuildingBlocksResponseDto
        {
            Sections = sections,
            MetaData = metaData
        };
    }
    
    private async Task<MaySucceed<(IEnumerable<ScreenSection> Sections, IEnumerable<IMetaData> MetaData)>> Execute(
        IScreenSectionProvider<TRequest> provider,
        IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        if (await IsDisabled(provider.ForSections)) 
            return (Enumerable.Empty<ScreenSection>(), Enumerable.Empty<IMetaData>());

        var rsp = await provider.Get(context, request, ct);

        if (rsp.IsFailed) return rsp.Failure;

        return (
            Sections: rsp.Value.Elements.Where(x => x.Element is not EmptyElement),
            MetaData: rsp.Value.MetaData
        );
    }

    private async Task<bool> IsDisabled(string[] sectionNames)
    {
        if (sectionNames.Length == 0) return false;
        if (sectionNames.Length == 1) return await sectionFeatureFlag.IsDisabled(sectionNames[0]);

        var tasks = new List<Task<bool>>();

        foreach (var section in sectionNames)
        {
            tasks.Add(sectionFeatureFlag.IsDisabled(section));
        }

        await Task.WhenAll(tasks);

        return tasks.All(x => x.Result);
    }
    
    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}