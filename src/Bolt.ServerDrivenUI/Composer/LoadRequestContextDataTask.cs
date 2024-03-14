using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Composer;


internal interface ILoadRequestContextDataTask<in TRequest>
{
    Task<MaySucceed.MaySucceed> Execute(IRequestContext context, TRequest request, CancellationToken ct);
}

internal sealed class LoadRequestContextDataTask<TRequest>(IRequestDataProvider requestDataProvider,
    IEnumerable<IRequestContextDataLoader<TRequest>> requestContextDataLoaders) : ILoadRequestContextDataTask<TRequest>
{
    public async Task<MaySucceed.MaySucceed> Execute(IRequestContext context, TRequest request, CancellationToken ct)
    {
        var requestData = requestDataProvider.Get();

        if (requestData.IsFailed) return requestData.Failure;
        
        context.Set(requestData.Value);
        
        await LoadContext(context, request, ct);

        return MaySucceed.MaySucceed.Ok();
    }
    

    private async Task LoadContext(IRequestContext context, TRequest request, CancellationToken ct)
    {
        foreach (var contextDataLoader in requestContextDataLoaders)
        {
            if (contextDataLoader.IsApplicable(request))
            {
                await contextDataLoader.Load(context, request, ct);
            }
        }
    }
}