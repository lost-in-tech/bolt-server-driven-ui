namespace Bolt.ServerDrivenUI.Core;

public interface IRequestContextWriter
{
    void Set<T>(string key, T value);
}