using System.Reflection;
using Bolt.Polymorphic.Serializer;
using Bolt.Sdui.Core;
using Ensemble.Core;
using Ensemble.Extensions.Web.LayoutProviders;
using Ensemble.Extensions.Web.RazorParser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ensemble.Extensions.Web;

public record EnsembleOptions
{
    public (Assembly Assembly, Type[] Types)[] TypesToScan { get; init; } = Array.Empty<(Assembly, Type[])>();
}

public static class IocSetup
{
    public static IServiceCollection AddEnsemble(this IServiceCollection source, EnsembleOptions? options = null)
    {
        options ??= new EnsembleOptions();

        Ensemble.IocSetup.AddEnsemble(source);
        
        source.AddHttpContextAccessor();
        source.AddRazorPages();
        source.AddRazorComponents();
        source.TryAddSingleton<IRequestDataProvider,RequestDataProvider>();
        source.TryAddSingleton<IRequestKeyNamesProvider,RequestKeyNamesProvider>();
        source.TryAddScoped<IRazorXmlViewParser,RazorXmlViewParser>();
        source.TryAddScoped<RazorViewRenderer>();
        
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