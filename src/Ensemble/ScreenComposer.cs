using Bolt.MaySucceed;
using Bolt.Sdui.Core;

namespace Ensemble.Core.Impl;

internal sealed class ScreenComposer<TRequest>(
        IRequestContext context,
        IRequestDataProvider requestDataProvider,
        IEnumerable<IRequestContextDataLoader<TRequest>> requestContextDataLoaders,
        IEnumerable<IScreenSectionsFilter<TRequest>> filters,
        IEnumerable<IScreenSectionsProvider<TRequest>> screenSectionsProviders,
        IEnumerable<ILayoutProvider<TRequest>> layoutProviders,
        IEnumerable<IResponseFilterDataLoader<TRequest>> processDataLoaders)
    : IScreenComposer<TRequest>
{
    public async Task<MaySucceed<Screen>> Compose(TRequest request, CancellationToken ct)
    {
        var requestData = requestDataProvider.Get();

        if (requestData.IsFailed) return requestData.Failure;
        
        context.Set(requestData.Value);
        
        await LoadContext(request, ct);

        var requestFilterRsp = await ApplyRequestFilter(request, ct);

        if (requestFilterRsp.IsFailed) return requestFilterRsp.Failure;

        request = requestFilterRsp.Value;

        var loadSectionsTask = GetSections(request, ct);
        var loadResponseFilterDataTask = LoadResponseFilterData(request, ct);
        var layoutRspTask = GetLayouts(request, ct);

        await Task.WhenAll(loadResponseFilterDataTask, loadSectionsTask, layoutRspTask);
        
        if (layoutRspTask.Result.IsFailed) return layoutRspTask.Result.Failure;
        
        var sectionsRsp = loadSectionsTask.Result;

        if (sectionsRsp.IsFailed) return sectionsRsp.Failure;

        var response = sectionsRsp.Value;

        if (loadResponseFilterDataTask.Result.IsFailed) return loadResponseFilterDataTask.Result.Failure;

        StoreFilterData(loadResponseFilterDataTask.Result.Value);

        var responseFilterRsp = await ApplyResponseFilter(request, response, ct);

        if (responseFilterRsp.IsFailed) return responseFilterRsp.Failure;

        response = responseFilterRsp.Value;

        return new Screen
        {
            Layouts = layoutRspTask.Result.Value,
            Sections = response.Sections,
            MetaData = response.MetaData
        };
    }

    private void StoreFilterData(List<ResponseFilterData> filterData)
    {
        foreach (var data in filterData)
        {
            context.Set(data.Key, data.Value);
        }
    }

    private async Task LoadContext(TRequest request, CancellationToken ct)
    {
        foreach (var contextDataLoader in requestContextDataLoaders)
        {
            if (contextDataLoader.IsApplicable(request))
            {
                await contextDataLoader.Load(context, request, ct);
            }
        }
    }

    private async Task<MaySucceed<List<ResponseFilterData>>> LoadResponseFilterData(TRequest request, CancellationToken ct)
    {
        var tasks = new List<Task<MaySucceed<ResponseFilterData>>>();

        foreach (var dataLoader in processDataLoaders)
        {
            tasks.Add(dataLoader.Load(context, request, ct));
        }

        await Task.WhenAll(tasks);

        var result = new List<ResponseFilterData>();
        
        foreach (var task in tasks)
        {
            if (task.Result.IsFailed) return task.Result.Failure;
            
            result.Add(task.Result.Value);
        }

        return result;
    }

    private async Task<MaySucceed<ScreenSectionResponseDto>> GetSections(TRequest request, CancellationToken ct)
    {
        var sections = new List<ScreenSection>();
        var metaData = new List<IMetaData>();
        foreach (var elementsProvider in screenSectionsProviders)
        {
            if (elementsProvider.IsApplicable(context, request))
            {
                var elementRsp = await elementsProvider.Get(context, request, ct);

                if (elementRsp.IsFailed) return elementRsp.Failure;
                
                sections.AddRange(elementRsp.Value.Sections);
                metaData.AddRange(elementRsp.Value.MetaData);
            }
        }

        return new ScreenSectionResponseDto
        {
            Sections = sections.ToArray(),
            MetaData = metaData.ToArray(),
        };
    }

    private async Task<MaySucceed<Dictionary<string, ScreenLayout>>> GetLayouts(TRequest request, CancellationToken ct)
    {
        var layouts = new Dictionary<string,ScreenLayout>();
        
        foreach (var layoutProvider in layoutProviders)
        {
            if (layoutProvider.IsApplicable(context, request))
            {
                var layoutRsp = await layoutProvider.Get(context, request, ct);

                if (layoutRsp.IsFailed) return layoutRsp.Failure;

                layouts[layoutRsp.Value.Name] = new ScreenLayout
                {
                    Element = layoutRsp.Value.Element,
                    VersionId = layoutRsp.Value.VersionId
                };
            }
        }

        return layouts;
    }

    private async Task<MaySucceed<ScreenSectionResponseDto>> ApplyResponseFilter(TRequest request, ScreenSectionResponseDto response,
        CancellationToken ct)
    {
        var lowPriorityFilters = filters.OrderByDescending(x => x.Priority);

        foreach (var filter in lowPriorityFilters)
        {
            if (filter.IsApplicable(context, request))
            {
                var filterRsp = await filter.OnResponse(context, request, response, ct);

                if (filterRsp.IsSucceed)
                {
                    response = filterRsp.Value;
                }
                else
                {
                    return filterRsp.Failure;
                }
            }
        }

        return response;
    }

    private async Task<MaySucceed<TRequest>> ApplyRequestFilter(TRequest request, CancellationToken ct)
    {
        var highPriorityFilters = filters.OrderBy(x => x.Priority);

        foreach (var filter in highPriorityFilters)
        {
            if (filter.IsApplicable(context, request))
            {
                var mayBeRequest = await filter.OnRequest(context, request, ct);

                if (mayBeRequest.IsSucceed)
                {
                    request = mayBeRequest.Value;
                }
                else
                {
                    return mayBeRequest.Failure;
                }
            }
        }

        return request;
    }
}