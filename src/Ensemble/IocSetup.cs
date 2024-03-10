using Ensemble.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ensemble;

public static class IocSetup
{
    public static IServiceCollection AddEnsemble(this IServiceCollection source)
    {
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenComposer<>), typeof(ScreenComposer<>)));
        source.TryAddScoped<IRequestContext, RequestContext>();
        source.TryAddScoped<IRequestContextReader>(x => x.GetRequiredService<IRequestContext>());

        return source;
    }
}