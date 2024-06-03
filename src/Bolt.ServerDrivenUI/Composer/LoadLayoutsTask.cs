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

        foreach (var layoutProvider in layoutProviders.OrderBy(x => x.Priority))
        {
            var layoutRsp = await layoutProvider.Get(context, request, ct);
                
            if (layoutRsp.IsFailed) return layoutRsp.Failure;

            foreach (var item in layoutRsp.Value)
            {
                layouts[item.Name] = new ScreenLayout
                {
                    Element = item.Element,
                    VersionId = item.VersionId,
                    NotModified = item.NotModified
                };
            }
        }

        return layouts;
    }
}