using Bolt.ServerDrivenUI.Composer;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bolt.ServerDrivenUI;

public static class IocSetup
{
    public static IServiceCollection AddEnsemble(this IServiceCollection source)
    {
        source.TryAddTransient<LoadScreenContextDataProviderTask>();
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenSectionsFilterTask<>), typeof(ScreenSectionsFilterTask<>)));
        source.TryAdd(ServiceDescriptor.Transient(typeof(ILoadResponseFilterDataTask<>), typeof(LoadResponseFilterDataTask<>)));
        source.TryAdd(ServiceDescriptor.Transient(typeof(ILoadScreenBuildingBlocksTask<>), typeof(LoadScreenBuildingBlocksTask<>)));
        source.TryAdd(ServiceDescriptor.Transient(typeof(ILoadLayoutsTask<>), typeof(LoadLayoutsTask<>)));
        source.TryAdd(ServiceDescriptor.Transient(typeof(ILoadRequestContextDataTask<>), typeof(LoadRequestContextDataTask<>)));
        source.TryAdd(ServiceDescriptor.Transient(typeof(IRequestValidationTask<>), typeof(RequestValidationTask<>)));
        
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenComposer<>), typeof(ScreenComposer<>)));
        source.TryAddScoped<IRequestContext, RequestContext>();
        source.TryAddScoped<IRequestContextReader>(x => x.GetRequiredService<IRequestContext>());
        source.TryAddSingleton<ISectionFeatureFlag, NullSectionFeatureFlag>();
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenBuildingBlocksProvider<>), typeof(DefaultScreenBuildingBlocksProvider<>)));
        source.TryAddSingleton<IAppInfoProvider,AppInfoProvider>();
        
        source.TryAddEnumerable(ServiceDescriptor.Transient<IScreenContextDataProvider,AppInfoScreenContextDataProvider>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IScreenContextDataProvider,RequestScreenContextDataProvider>());
        
        return source;
    }
}