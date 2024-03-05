namespace Ensemble.Core;

public interface IRequestContextReader
{
    T? TryGet<T>(string key);
}