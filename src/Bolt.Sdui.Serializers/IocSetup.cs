using Bolt.Sdui.Core;
using Bolt.Sdui.Serializers.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Bolt.Sdui.Serializers;

public static class IocSetup
{
    private static readonly Type[] DefaultTypesToScan = new[] 
    { 
        typeof(IElement), 
        typeof(IMetaData), 
        typeof(IUIAction) 
    };

    public static IServiceCollection AddSerializers(
        this IServiceCollection services, 
        SetupSerializersOption? option = null)
    {
        option = option ?? new SetupSerializersOption();

        option = option with 
        { 
            TypesToScan = option.TypesToScan
                                .Concat(DefaultTypesToScan)
                                .ToArray() 
        };

        services.TryAddSingleton<ITypeRegistry>(sc =>
        {
            var instance = new TypeRegistry();
            
            foreach (var assembly in option.AssembliesToScan)
            {
                instance.Register(assembly, option.TypesToScan);
            }

            return instance;
        });

        services.TryAddSingleton<XmlDeserializer>();
        services.TryAddSingleton<ISduiXmlSerializer, SduiXmlSerializer>();

        return services;
    }
}

public record SetupSerializersOption
{
    public Type[] TypesToScan { get; init; } = Array.Empty<Type>();
    public Assembly[] AssembliesToScan { get; init; } = Array.Empty<Assembly>();
}
