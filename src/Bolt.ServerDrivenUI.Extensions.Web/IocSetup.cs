using System.Reflection;
using Bolt.Polymorphic.Serializer;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;
using Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public record ServerDrivenWebOptions
{
    /// <summary>
    /// Pass the assembly and types to scan for the support of polymorphic serialization. If you don't pass any type then
    /// we only look for default types e.g. IElement, IUIAction, IMetadata
    /// </summary>
    public (Assembly Assembly, Type[] Types)[] TypesToScanForPolymorphicSerialization { get; init; } = Array.Empty<(Assembly, Type[])>();
    public bool EnableFeatureFlag { get; init; } = false;
    public string FeatureFlagSectionName { get; init; } = "Bolt:ServerDrivenUI:FeatureFlags";
    public string RazorLayoutSettingsSectionName { get; init; } = "Bolt:ServerDrivenUI:RazorLayoutSettings";
    public string AppInfoSettingsSectionName { get; init; } = "Bolt:ServerDrivenUI:App";
}

public static class IocSetup
{
    public static IServiceCollection AddServerDrivenUiForWeb(this IServiceCollection source, IConfiguration configuration, ServerDrivenWebOptions? options = null)
    {
        options ??= new ServerDrivenWebOptions();

        if (options.EnableFeatureFlag)
        {
            source.Configure<SectionsFeatureFlagSettings>(settings => configuration.GetSection(options.FeatureFlagSectionName).Bind(settings));
            source.TryAddTransient<ISectionFeatureFlag, SectionFeatureFlag>();
        }

        source.AddServerDrivenUiExternalSource(configuration);
        
        source.Configure<RazorLayoutProviderSettings>(settings =>
            configuration.GetSection(options.RazorLayoutSettingsSectionName).Bind(settings));
        
        source.Configure<AppInfoSettings>(settings =>
            configuration.GetSection(options.AppInfoSettingsSectionName).Bind(settings));
        
        source.TryAddSingleton<IAppInfoProvider, AppInfoProvider>();

        source.AddServerDrivenUi();
        
        source.AddHttpContextAccessor();
        source.AddRazorPages();
        source.AddRazorComponents();
        source.TryAddSingleton<IRequestDataProvider,RequestDataProvider>();
        source.TryAddSingleton<IRequestKeyNamesProvider,RequestKeyNamesProvider>();
        source.TryAddScoped<IRazorXmlViewParser,RazorXmlViewParser>();
        source.TryAddScoped<IRazorViewReader,RazorViewReader>();
        source.TryAddSingleton<IHttpRequestWrapper, HttpRequestWrapper>();
        
        
        source.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(ILayoutFileNameProvider<>), typeof(DefaultLayoutFileNameProvider<>)));
        source.TryAddEnumerable(ServiceDescriptor.Scoped(typeof(ILayoutProvider<>), typeof(RazorLayoutProvider<>)));

        source.AddPolymorphicSerializer((opt) =>
        {
            opt.AddSupportedType(typeof(IElement).Assembly, 
                typeof(IElement), 
                typeof(IUiAction), 
                typeof(IMetaData));

            foreach (var typesToScan in options.TypesToScanForPolymorphicSerialization)
            {
                opt.AddSupportedType(
                    typesToScan.Assembly, 
                    typesToScan.Types.Length > 0 
                        ? typesToScan.Types : 
                        [typeof(IElement), typeof(IUiAction), typeof(IMetaData)]
                );
            }
        });
        
        source.TryAddTransient<IScreenViewResultComposer, ScreenViewResultComposer>();
        source.TryAddTransient<IScreenEndpointResultComposer, ScreenViewResultComposer>();
        
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpRequestHeadersProvider, CookieHeaderForExternalRequest>());
        
        return source;
    }
}