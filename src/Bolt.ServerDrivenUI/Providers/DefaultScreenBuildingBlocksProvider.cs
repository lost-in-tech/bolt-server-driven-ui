using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Microsoft.Extensions.Logging;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class DefaultScreenBuildingBlocksProvider<TRequest>(
        IEnumerable<IScreenSectionProvider<TRequest>> sectionProviders,
        ISectionFeatureFlag sectionFeatureFlag,
        IEnumerable<IExternalScreenRequestProvider<TRequest>> externalScreenRequestProviders,
        IExternalScreenProvider externalScreenProvider,
        ILogger<DefaultScreenBuildingBlocksProvider<TRequest>> logger)
    : IScreenBuildingBlocksProvider<TRequest>
{
    public async Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var tasks = new List<Task<MaySucceed<(IEnumerable<ScreenSection> Sections, IEnumerable<IMetaData> MetaData)>>>();
        var requestData = context.RequestData();
        var lazySectionNames = new List<string>();
        
        foreach (var provider in sectionProviders)
        {
            if (provider.IsApplicable(context, request) == false) continue;
            
            var forSections = provider.ForSections(context, request);

            foreach (var section in forSections)
            {
                if (section.Scope == SectionScope.Lazy)
                {
                    lazySectionNames.Add(section.Name);
                }
            }
            
            if(forSections.IsApplicable(requestData) == false) continue;
            
            tasks.Add(Execute(provider, forSections, context, request, ct));
        }

        foreach (var externalScreenRequestProvider in externalScreenRequestProviders)
        {
            var requests = externalScreenRequestProvider.Get(context, request);

            foreach (var req in requests)
            {
                tasks.Add(Execute(req, context, ct));
            }
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
            MetaData = metaData,
            LazySectionNames = lazySectionNames
        };
    }

    private async Task<MaySucceed<(IEnumerable<ScreenSection> Sections, IEnumerable<IMetaData> MetaData)>> Execute(
        ExternalScreenRequest request, IRequestContextReader context, CancellationToken ct)
    {
        if (await IsDisabled(request.ForSections)) 
            return (Enumerable.Empty<ScreenSection>(), Enumerable.Empty<IMetaData>());

        try
        {
            var rsp = await externalScreenProvider.Get(context, request, ct);

            if (rsp.IsFailed || rsp.Value == null)
                return (Enumerable.Empty<ScreenSection>(), Enumerable.Empty<IMetaData>());

            return (
                Sections: rsp.Value.Sections,
                MetaData: rsp.Value.MetaData ?? Enumerable.Empty<IMetaData>()
            );
        }
        catch (Exception e)
        {
            logger.LogError(e, "{provider} failed with exception {errorMessage}", externalScreenProvider.GetType().FullName, e.Message);
        }
        
        return (Sections: Enumerable.Empty<ScreenSection>(),
            MetaData: Enumerable.Empty<IMetaData>());
    }
    
    private async Task<MaySucceed<(IEnumerable<ScreenSection> Sections, IEnumerable<IMetaData> MetaData)>> Execute(
        IScreenSectionProvider<TRequest> provider,
        SectionInfo[] forSections, 
        IRequestContextReader context,
        TRequest request,
        CancellationToken ct)
    {
        if (await IsDisabled(forSections)) 
            return (Enumerable.Empty<ScreenSection>(), Enumerable.Empty<IMetaData>());

        try
        {
            var rsp = await provider.Get(context, request, ct);

            if (rsp.IsSucceed)
            {
                return (
                    Sections: rsp.Value.Sections.Where(x => x.Element is not EmptyElement),
                    MetaData: rsp.Value.MetaData
                );
            }

            var providerTypeData = TypeHelper.Get(provider.GetType());
            
            logger.LogError("{provider} failed with {failure}", providerTypeData.Name, rsp.Failure);

            if (providerTypeData.HasMustSucceedAttribute)
            {
                return rsp.Failure;
            }
        }
        catch (Exception e)
        {
            var providerTypeData = TypeHelper.Get(provider.GetType());
            
            logger.LogError(e, "{provider} failed with exception {errorMessage}", provider.GetType().FullName, e.Message);
            
            if (providerTypeData.HasMustSucceedAttribute)
            {
                return HttpFailure.InternalServerError();
            }
        }

        return (Sections: Enumerable.Empty<ScreenSection>(),
            MetaData: Enumerable.Empty<IMetaData>());
    }

    private async Task<bool> IsDisabled(SectionInfo[] sections)
    {
        if (sections.Length == 0) return false;
        if (sections.Length == 1) return await sectionFeatureFlag.IsDisabled(sections[0].Name);

        var tasks = new List<Task<bool>>();

        foreach (var section in sections)
        {
            tasks.Add(sectionFeatureFlag.IsDisabled(section.Name));
        }

        await Task.WhenAll(tasks);

        return tasks.All(x => x.Result);
    }
    
    public bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}