using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bolt.ServerDrivenUI;

public static class IocSetup
{
    public static IServiceCollection AddEnsemble(this IServiceCollection source)
    {
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenComposer<>), typeof(ScreenComposer<>)));
        source.TryAddScoped<IRequestContext, RequestContext>();
        source.TryAddScoped<IRequestContextReader>(x => x.GetRequiredService<IRequestContext>());
        source.TryAddSingleton<ISectionFeatureFlag, NullSectionFeatureFlag>();
        source.TryAdd(ServiceDescriptor.Transient(typeof(IScreenBuildingBlocksProvider<>), typeof(DefaultScreenBuildingBlocksProvider<>)));
        source.TryAddSingleton<IAppInfoProvider,AppInfoProvider>();
        source.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IScreenSectionProvider<>), typeof(AppInfoMetaDataProvider<>)));
        source.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IScreenSectionProvider<>), typeof(RequestContextMetaDataProvider<>)));
        
        return source;
    }
}