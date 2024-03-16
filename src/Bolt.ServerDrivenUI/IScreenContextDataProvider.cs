using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

internal interface IScreenContextDataProvider
{
    IEnumerable<(string Key, object? Value)> Get(IRequestContextReader context);
}