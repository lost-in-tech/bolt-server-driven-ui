using System.Reflection;
using Bolt.Polymorphic.Serializer;
using Bolt.Sdui.Core;
using Ensemble.Core;
using Ensemble.Extensions.Web.LayoutProviders;
using Ensemble.Extensions.Web.RazorParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ensemble.Extensions.Web;

public record EnsembleOptions
{
    public (Assembly Assembly, Type[] Types)[] TypesToScan { get; init; } = Array.Empty<(Assembly, Type[])>();
    public bool EnableFeatureFlag { get; init; } = false;
    public string FeatureFlagSectionName { get; init; } = "Ensemble:FeatureFlags";
}

public static class IocSetup
{
    public static IServiceCollection AddEnsemble(this IServiceCollection source, IConfiguration configuration, EnsembleOptions? options = null)
    {
        options ??= new EnsembleOptions();

        if (options.EnableFeatureFlag)
        {
            source.Configure<SectionsFeatureFlagSettings>(settings => configuration.GetSection(options.FeatureFlagSectionName).Bind(settings));
            source.TryAddTransient<ISectionFeatureFlag, SectionFeatureFlag>();
        }

        source.AddEnsemble();
        
        source.AddHttpContextAccessor();
        source.AddRazorPages();
        source.AddRazorComponents();
        source.TryAddSingleton<IRequestDataProvider,RequestDataProvider>();
        source.TryAddSingleton<IRequestKeyNamesProvider,RequestKeyNamesProvider>();
        source.TryAddScoped<IRazorXmlViewParser,RazorXmlViewParser>();
        source.TryAddScoped<RazorViewRenderer>();
        source.TryAddSingleton<IHttpRequestWrapper, HttpRequestWrapper>();
        
        source.TryAddEnumerable(ServiceDescriptor.Scoped(typeof(ILayoutProvider<>), typeof(RazorWideLayoutProvider<>)));
        source.TryAddEnumerable(ServiceDescriptor.Scoped(typeof(ILayoutProvider<>), typeof(RazorCompactLayoutProvider<>)));

        source.AddPolymorphicSerializer((opt) =>
        {
            opt.AddSupportedType(typeof(IElement).Assembly, 
                typeof(IElement), 
                typeof(IUiAction), 
                typeof(IMetaData));

            foreach (var typesToScan in options.TypesToScan)
            {
                opt.AddSupportedType(typesToScan.Assembly, typesToScan.Types);
            }
        });
        
        source.TryAddTransient<IScreenViewResultComposer, ScreenViewResultComposer>();
        
        return source;
    }
}