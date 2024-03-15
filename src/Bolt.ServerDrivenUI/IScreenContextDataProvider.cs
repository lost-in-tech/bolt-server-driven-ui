using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IScreenContextDataProvider
{
    IEnumerable<(string Key, object? Value)> Get(IRequestContextReader context);
}