using System.Collections.Concurrent;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web.LayoutProviders;

public static class LayoutStore
{
    private static ConcurrentDictionary<string, LayoutResponse> _store = new(StringComparer.OrdinalIgnoreCase);
    
    public static LayoutResponse? TryGet<T>(string fileName, string version)
    {
        return _store.TryGetValue(Key<T>(fileName, version), out var value) ? value : null;
    }

    public static LayoutResponse Set<T>(string fileName, string version, LayoutResponse value)
    {
        return _store.AddOrUpdate(Key<T>(fileName, version), (_) => value, (key, _) => value);
    }

    private static string Key<T>(string fileName, string version) => $"{typeof(T).FullName}-{fileName}-{version}";
}