namespace Bolt.ServerDrivenUI.Core;

public interface IRequestContextReader
{
    T? TryGet<T>(string key);
}