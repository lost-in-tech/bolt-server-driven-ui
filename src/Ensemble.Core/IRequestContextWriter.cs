namespace Ensemble.Core;

public interface IRequestContextWriter
{
    void Set<T>(string key, T value);
}