using System.Collections.Concurrent;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

public static class LayoutStore
{
    private static ConcurrentDictionary<string, LayoutResponse> _store = new(StringComparer.OrdinalIgnoreCase);
    
    public static LayoutResponse? TryGet<T>(string fileName)
    {
        return _store.TryGetValue(Key<T>(fileName), out var value) ? value : null;
    }

    public static LayoutResponse Set<T>(string fileName, LayoutResponse value)
    {
        return _store.AddOrUpdate(Key<T>(fileName), (_) => value, (key, _) => value);
    }

    private static string Key<T>(string fileName) => $"{typeof(T).FullName}-{fileName}";
}