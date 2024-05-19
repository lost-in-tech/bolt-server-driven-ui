using Bolt.ServerDrivenUI.Core;
using Bolt.Endeavor;
using Microsoft.Extensions.Logging;

namespace Bolt.ServerDrivenUI.Composer;

internal interface IScreenSectionsFilterTask<TRequest>
{
    Task<MaySucceed<TRequest>> Execute(IRequestContext context, TRequest request, CancellationToken ct);

    Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Execute(IRequestContext context, TRequest request,
        ScreenBuildingBlocksResponseDto response,
        CancellationToken ct);
}

internal sealed class ScreenSectionsFilterTask<TRequest>(
    IEnumerable<IScreenSectionsFilter<TRequest>> filters,
    ILogger<ScreenSectionsFilterTask<TRequest>> logger) : IScreenSectionsFilterTask<TRequest>
{
    
    public async Task<MaySucceed<TRequest>> Execute(IRequestContext context, TRequest request, CancellationToken ct)
    {
        var highPriorityFilters = filters.OrderBy(x => x.Priority);

        foreach (var filter in highPriorityFilters)
        {
            if (filter.IsApplicable(context, request))
            {
                try
                {
                    var mayBeRequest = await filter.OnRequest(context, request, ct);

                    if (mayBeRequest.IsSucceed)
                    {
                        request = mayBeRequest.Value;
                    }
                    else
                    {
                        var typeData = TypeHelper.Get(filter.GetType());
                        
                        logger.LogError("{provider} failed with {failure}", typeData.Name, mayBeRequest.Failure);

                        if (typeData.HasMustSucceedAttribute)
                        {
                            return mayBeRequest.Failure;
                        }
                    }
                }
                catch (Exception e)
                {
                    var typeData = TypeHelper.Get(filter.GetType());
                    
                    logger.LogError(e, "{requestFilter} failed with exception {errorMessage}", typeData.Name, e.Message);  
                    
                    if (typeData.HasMustSucceedAttribute)
                    {
                        return HttpFailure.InternalServerError();
                    }
                }
            }
        }

        return request;
    }

    public async Task<MaySucceed<ScreenBuildingBlocksResponseDto>> Execute(IRequestContext context, TRequest request,
        ScreenBuildingBlocksResponseDto response,
        CancellationToken ct)
    {
        var lowPriorityFilters = filters.OrderByDescending(x => x.Priority);

        foreach (var filter in lowPriorityFilters)
        {
            if (filter.IsApplicable(context, request))
            {
                try
                {
                    var filterRsp = await filter.OnResponse(context, request, response, ct);

                    if (filterRsp.IsSucceed)
                    {
                        response = filterRsp.Value;
                    }
                    else
                    {
                        var typeData = TypeHelper.Get(filter.GetType());
                        
                        logger.LogError("{provider} failed with {failure}", typeData.Name, filterRsp.Failure);

                        if (typeData.HasMustSucceedAttribute)
                        {
                            return filterRsp.Failure;
                        }
                    }
                }
                catch (Exception e)
                {
                    var typeData = TypeHelper.Get(filter.GetType());
                    
                    logger.LogError(e, "{requestFilter} failed with exception {errorMessage}", typeData.Name, e.Message);  
                    
                    if (typeData.HasMustSucceedAttribute)
                    {
                        return HttpFailure.InternalServerError();
                    }
                }
            }
        }

        return response;
    }
}