using System.Reflection;

namespace Bolt.Sdui.Serializers;

internal interface ITypeRegistry
{
    TypeData? TryGet(string typeName);
    TypeData? TryGet(Type type);
}

internal sealed class TypeRegistry : ITypeRegistry
{
    private readonly Dictionary<string, TypeData> _typeStoreByName = new();
    private readonly Dictionary<Type, TypeData> _typeStoreByType = new();

    internal TypeRegistry(){}

    public TypeData? TryGet(string typeName)
    {
        return _typeStoreByName.TryGetValue(typeName, out var typeData) ? typeData : null;
    }

    public TypeData? TryGet(Type type)
    {
        return _typeStoreByType.TryGetValue(type, out var typeData) ? typeData : null;
    }

    internal void Register(Assembly assembly, Type[] typesToScan)
    {
        foreach(var type in assembly.GetTypes())
        {
            if(type.IsClass)
            {
                if(typesToScan.Any(t => type.IsAssignableTo(t)))
                {
                    var typeData = BuildTypeData(type);

                    _typeStoreByName[type.Name] = typeData;
                    _typeStoreByType[type] = typeData;
                }
            }
        }
    }

    private TypeData BuildTypeData(Type type)
    {
        var isSimpleType = TypeHelper.IsSimpleType(type);

        var properties = new Dictionary<string, PropertyData>();

        if(!isSimpleType)
        {
            var typeProps = type.GetProperties();

            foreach (var property in typeProps)
            {
                properties[property.Name] = new PropertyData
                {
                    TypeData = BuildTypeData(property.PropertyType),
                    PropertyInfo = property
                };
            }
        }

        var collectionType = type.GetElementType();
        return new TypeData
        {
            Type = type,
            IsArray = type.IsArray,
            IsEnum = type.IsEnum,
            IsSimpleType = isSimpleType,
            CollectionType = type.IsArray && collectionType != null ? BuildTypeData(collectionType) : null,
            Properties = properties
        };
    }
}

public record TypeData
{
    public required Type Type { get; init; }
    public bool IsSimpleType { get; init; }
    public bool IsEnum { get; init; }
    public bool IsArray { get; init; }
    public TypeData? CollectionType { get; init; }
    public required Dictionary<string, PropertyData> Properties { get; init; }
    public PropertyData? TryGetProperty(string name) => Properties == null 
                                                            ? null : Properties.TryGetValue(name, out var pt) 
                                                                ? pt : null;

}

public record PropertyData
{
    public required PropertyInfo PropertyInfo { get; init; }
    public required TypeData TypeData { get; init; }
}
