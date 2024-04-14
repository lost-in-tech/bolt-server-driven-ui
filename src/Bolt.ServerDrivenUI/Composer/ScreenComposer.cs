using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Composer;

internal sealed class ScreenComposer<TRequest>(
        IRequestContext context,
        ILoadRequestContextDataTask<TRequest> loadRequestContextDataTask,
        IRequestValidationTask<TRequest> requestValidationTask,
        ILoadLayoutsTask<TRequest> loadLayoutsTask,
        IScreenSectionsFilterTask<TRequest> screenSectionsFilterTask,
        ILoadScreenBuildingBlocksTask<TRequest> loadScreenBuildingBlocksTask,
        ILoadResponseFilterDataTask<TRequest> responseFilterDataLoaderTask,
        LoadScreenContextDataProviderTask loadScreenContextDataProviderTask)
    : IScreenComposer<TRequest>
{
    private static readonly MaySucceed<Dictionary<string, ScreenLayout>> EmptyLayout =
        MaySucceed<Dictionary<string, ScreenLayout>>.Ok(new Dictionary<string, ScreenLayout>());
    
    public async Task<MaySucceed<Screen>> Compose(TRequest request, CancellationToken ct)
    {
        var contextLoadTaskRsp = await loadRequestContextDataTask.Execute(context, request, ct);

        if (contextLoadTaskRsp.IsFailed) return contextLoadTaskRsp.Failure;
        
        var requestFilterRsp = await screenSectionsFilterTask.Execute(context, request, ct);

        if (requestFilterRsp.IsFailed) return requestFilterRsp.Failure;

        request = requestFilterRsp.Value;

        var validationRsp = await requestValidationTask.Execute(context, request, ct);

        if (validationRsp.IsFailed) return validationRsp.Failure;

        var requestMode = context.RequestData().Mode;
        
        var loadSectionsTask = loadScreenBuildingBlocksTask.Execute(context, request, ct);
        var loadResponseFilterDataTask = responseFilterDataLoaderTask.Execute(context, request, ct);
        var layoutRspTask = requestMode == RequestMode.Default 
            ? loadLayoutsTask.Execute(context, request, ct)
            : Task.FromResult(EmptyLayout);

        await Task.WhenAll(loadResponseFilterDataTask, loadSectionsTask, layoutRspTask);
        
        if (layoutRspTask.Result.IsFailed) return layoutRspTask.Result.Failure;
        
        var sectionsRsp = loadSectionsTask.Result;

        if (sectionsRsp.IsFailed) return sectionsRsp.Failure;

        var response = sectionsRsp.Value;

        if (loadResponseFilterDataTask.Result.IsFailed) return loadResponseFilterDataTask.Result.Failure;

        StoreFilterData(loadResponseFilterDataTask.Result.Value);

        var responseFilterRsp = await screenSectionsFilterTask.Execute(context, request, response, ct);

        if (responseFilterRsp.IsFailed) return responseFilterRsp.Failure;

        response = responseFilterRsp.Value;

        return new Screen
        {
            Layouts = layoutRspTask.Result.Value.Count == 0 ? null : layoutRspTask.Result.Value,
            Sections = response.Sections,
            MetaData = response.MetaData.Any() ? response.MetaData : null,
            ContextData = loadScreenContextDataProviderTask.Execute(context, response.LazySectionNames)
        };
    }

    private void StoreFilterData(List<ResponseFilterData> filterData)
    {
        foreach (var data in filterData)
        {
            context.Set(data.Key, data.Value);
        }
    }
}