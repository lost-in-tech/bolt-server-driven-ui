using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

public interface ILayoutFileNameProvider<in TRequest>
{
    string Get(IRequestContextReader context, TRequest request);
    int Priority { get; }
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

internal static class LayoutFileNameProviderExtensions
{
    public static string GetFileNameApplied<T>(this IEnumerable<ILayoutFileNameProvider<T>> source,
        IRequestContextReader context,
        T request)
    {
        var sorted = source.OrderBy(x => x.Priority);

        foreach (var provider in sorted)
        {
            if (provider.IsApplicable(context, request))
            {
                return provider.Get(context, request);
            }
        }

        return $"{context.RequestData().ScreenSize}";
    }
}

public abstract class LayoutFileNameProvider<TRequest> : ILayoutFileNameProvider<TRequest>
{
    public abstract string Get(IRequestContextReader context, TRequest request);
    public virtual int Priority => 0;
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}

internal sealed class DefaultLayoutFileNameProvider<TRequest> : LayoutFileNameProvider<TRequest>
{
    public override string Get(IRequestContextReader context, TRequest request)
    {
        return $"{context.RequestData().ScreenSize ?? RequestScreenSize.Wide}";
    }

    public override int Priority => 100;
}