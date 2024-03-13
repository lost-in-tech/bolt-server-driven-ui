using System.Collections.Concurrent;
using System.Reflection;

namespace Bolt.ServerDrivenUI;

internal static class TypeHelper
{
    private static ConcurrentDictionary<Type, TypeData> _store = new ConcurrentDictionary<Type, TypeData>();
    
    public static TypeData Get(Type type)
    {
        return _store.GetOrAdd(type, (tp) =>
        {
            var customAttributes = tp.GetCustomAttributes();
            return new TypeData
            {
                Name = tp.FullName ?? tp.Name,
                HasMainAttribute = customAttributes.Any(ct => ct is MainProviderAttribute)
            };
        });
    }
}

internal record TypeData
{
    public required string Name { get; init; }
    public bool HasMainAttribute { get; init; }
}