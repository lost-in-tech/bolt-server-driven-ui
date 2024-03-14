using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Composer;

internal interface ILoadScreenBuildingBlocksTask<in TRequest>
{
    Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Execute(IRequestContextReader context, TRequest request, CancellationToken ct);
}

internal sealed class LoadScreenBuildingBlocksTask<TRequest>(IEnumerable<IScreenBuildingBlocksProvider<TRequest>> screenSectionsProviders) : ILoadScreenBuildingBlocksTask<TRequest>
{
    public async Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Execute(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var sections = new List<ScreenSection>();
        var metaData = new List<IMetaData>();

        var tasks = new List<Task<MaySucceed<ScreenBuildingBlocksResponseDto>>>();
        
        foreach (var elementsProvider in screenSectionsProviders)
        {
            if (elementsProvider.IsApplicable(context, request))
            {
                tasks.Add(elementsProvider.Get(context, request, ct));
            }
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            var providerRsp = task.Result;
            
            if (providerRsp.IsFailed) return providerRsp.Failure;
                
            sections.AddRange(providerRsp.Value.Sections);
            metaData.AddRange(providerRsp.Value.MetaData);
        }

        return new ScreenBuildingBlocksResponseDto
        {
            Sections = sections,
            MetaData = metaData,
        };
    }
}