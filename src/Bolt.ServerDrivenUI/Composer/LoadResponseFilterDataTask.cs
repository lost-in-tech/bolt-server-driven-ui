using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.Logging;

namespace Bolt.ServerDrivenUI.Composer;

internal interface ILoadResponseFilterDataTask<in TRequest>
{
    Task<MaySucceed<List<ResponseFilterData>>> Execute(IRequestContext context, TRequest request, CancellationToken ct);
}

internal sealed class LoadResponseFilterDataTask<TRequest>(
    IEnumerable<IResponseFilterDataLoader<TRequest>> processDataLoaders,
    ILogger<LoadResponseFilterDataTask<TRequest>> logger) : ILoadResponseFilterDataTask<TRequest>
{
    public async Task<MaySucceed<List<ResponseFilterData>>> Execute(IRequestContext context, TRequest request, CancellationToken ct)
    {
        var tasks = new List<Task<MaySucceed<MayBe<ResponseFilterData>>>>();

        foreach (var dataLoader in processDataLoaders)
        {
            tasks.Add(Execute(dataLoader, context, request, ct));
        }

        await Task.WhenAll(tasks);

        var result = new List<ResponseFilterData>();
        
        foreach (var task in tasks)
        {
            var maySucceedRsp = task.Result;
            
            if (maySucceedRsp.IsFailed) return maySucceedRsp.Failure;

            var mayHaveValue = maySucceedRsp.Value;
            
            if (!mayHaveValue.IsNone)
            {
                result.Add(mayHaveValue.Value);
            }
        }

        return result;
    }
    
    private async Task<MaySucceed<MayBe<ResponseFilterData>>> Execute(IResponseFilterDataLoader<TRequest> filterDataLoader, IRequestContext context, TRequest request, CancellationToken ct)
    {
        try
        {
            var rsp = await filterDataLoader.Load(context, request, ct);

            if (rsp.IsSucceed) return new MayBe<ResponseFilterData>(rsp.Value);
            
            var typeData = TypeHelper.Get(filterDataLoader.GetType());
            
            logger.LogError("{filterDataLoader} failed with {failure}", typeData.Name, rsp.Failure);

            if (typeData.HasMustSucceedAttribute) return rsp.Failure;
        }
        catch (Exception e)
        {
            var typeData = TypeHelper.Get(filterDataLoader.GetType());
            
            logger.LogError(e, "{filterDataLoader} failed with exception {errorMessage}", typeData.Name, e.Message); 

            if (typeData.HasMustSucceedAttribute)
            {
                return HttpFailure.InternalServerError();
            }
        }

        return new MayBe<ResponseFilterData>();
    }
}