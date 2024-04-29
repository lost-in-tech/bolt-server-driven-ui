using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

public interface ILayoutFileNameProvider<in TRequest>
{
    IEnumerable<(string LayoutName, string FileName)> Get(IRequestContextReader context, TRequest request);
    int Priority { get; }
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

internal static class LayoutFileNameProviderExtensions
{
    public static IEnumerable<(string LayoutName, string FileName)> GetLayoutFileNames<T>(this IEnumerable<ILayoutFileNameProvider<T>> source,
        IRequestContextReader context,
        T request)
    {
        var sorted = source.OrderBy(x => x.Priority);
        
        foreach (var provider in sorted)
        {
            if (!provider.IsApplicable(context, request)) continue;

            var data = provider.Get(context, request);

            foreach (var item in data)
            {
                yield return item;
            }
                
            yield break;
        }
    }
}

public abstract class LayoutFileNameProvider<TRequest> : ILayoutFileNameProvider<TRequest>
{
    public abstract IEnumerable<(string LayoutName, string FileName)> Get(IRequestContextReader context, TRequest request);
    public virtual int Priority => 0;
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

internal sealed class DefaultLayoutFileNameProvider<TRequest> : LayoutFileNameProvider<TRequest>
{
    public override IEnumerable<(string LayoutName, string FileName)> Get(IRequestContextReader context, TRequest request)
    {
        if (context.RequestData().ScreenSize != RequestScreenSize.Compact)
        {
            yield return ("wide", "wide");
        }

        yield return ("compact", "compact");
    }

    public override int Priority => 100;
}