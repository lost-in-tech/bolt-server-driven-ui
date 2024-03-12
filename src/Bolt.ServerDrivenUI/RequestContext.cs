using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

internal sealed class RequestContext : IRequestContext
{
    private readonly Dictionary<string, object> _store = new Dictionary<string, object>();
    
    public void Set<T>(string key, T value)
    {
        _store[key] = value!;
    }

    public T? TryGet<T>(string key)
    {
        if (_store.TryGetValue(key, out var value))
        {
            return (T)value;
        }

        return default;
    }
}