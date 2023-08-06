using Bolt.Sdui.Core;
using Bolt.Sdui.Serializers.Json;
using Bolt.Sdui.Serializers.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using System.Text.Json;

namespace Bolt.Sdui.Serializers;

public static class IocSetup
{
    private static readonly Type[] DefaultTypesToScan = new[] 
    { 
        typeof(IElement), 
        typeof(IMetaData), 
        typeof(IUiAction) 
    };

    public static IServiceCollection AddSerializers(
        this IServiceCollection services, 
        SetupSerializersOption? option = null)
    {
        option = BuildOption(option);

        var typeRegistry = BuildTypeRegistry(option);

        services.TryAddSingleton(typeRegistry);

        AddXmlSerializer(services);
        AddJsonSerializer(services, typeRegistry);

        return services;
    }

    private static void AddJsonSerializer(IServiceCollection services, ITypeRegistry typeRegistry)
    {
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.ApplyUDLStandard(typeRegistry);
        DefaultJsonSerializerOption.SetDefault(jsonOptions);

        services.TryAddSingleton<ISduiJsonSerializer, SduiJsonSerializer>();
        services.TryAddSingleton<IUdlJsonOptionsProvider, UdlJsonOptionsProvider>();
    }

    private static void AddXmlSerializer(IServiceCollection services)
    {
        services.TryAddSingleton<XmlDeserializer>();
        services.TryAddSingleton<XmlSerializer>();
        services.TryAddSingleton<ISduiXmlSerializer, SduiXmlSerializer>();
    }

    private static SetupSerializersOption BuildOption(SetupSerializersOption? option)
    {
        option = option ?? new SetupSerializersOption();

        option = option with
        {
            TypesToScan = option.TypesToScan
                                .Concat(DefaultTypesToScan)
                                .ToArray()
        };

        return option;
    }

    private static ITypeRegistry BuildTypeRegistry(SetupSerializersOption option)
    {
        var typeRegistry = new TypeRegistry();

        foreach (var assembly in option.AssembliesToScan)
        {
            typeRegistry.Register(assembly, option.TypesToScan);
        }

        return typeRegistry;
    }
}

public record SetupSerializersOption
{
    public Type[] TypesToScan { get; init; } = Array.Empty<Type>();
    public Assembly[] AssembliesToScan { get; init; } = Array.Empty<Assembly>();
}
