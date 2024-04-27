using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Composer;

internal interface ILoadLayoutsTask<in TRequest>
{
    Task<MaySucceed<Dictionary<string, ScreenLayout>>> Execute(
        IRequestContextReader context,
        TRequest request,
        CancellationToken ct);
}

internal sealed class LoadLayoutsTask<TRequest>(IEnumerable<ILayoutProvider<TRequest>> layoutProviders) : ILoadLayoutsTask<TRequest>
{
    public async Task<MaySucceed<Dictionary<string, ScreenLayout>>> Execute(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var layouts = new Dictionary<string,ScreenLayout>();

        var tasks = new List<Task<MaySucceed<LayoutResponse>>>();
        
        foreach (var layoutProvider in layoutProviders)
        {
            if (layoutProvider.IsApplicable(context, request))
            {
                tasks.Add(layoutProvider.Get(context, request, ct));
            }
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            var layoutRsp = task.Result;
            
            if (layoutRsp.IsFailed) return layoutRsp.Failure;
            
            layouts[layoutRsp.Value.Name] = new ScreenLayout
            {
                Element = layoutRsp.Value.Element,
                VersionId = layoutRsp.Value.VersionId,
                NotModified = layoutRsp.Value.NotModified
            };
        }

        return layouts;
    }
}