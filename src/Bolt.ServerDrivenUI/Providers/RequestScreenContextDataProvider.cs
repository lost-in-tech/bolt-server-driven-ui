using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class RequestScreenContextDataProvider : IScreenContextDataProvider
{
    public IEnumerable<(string Key, object? Value)> Get(IRequestContextReader context)
    {
        var requestData = context.RequestData();

        yield return ("request", new
        {
            Cid = requestData.CorrelationId,
            Tenant = requestData.Tenant,
            App = requestData.App,
            RootApp = requestData.RootApp,
            Mode = requestData.Mode
        });
    }
}

public record RequestContextMetaData : IMetaData
{
    public required string CorrelationId { get; init; }
    public required string App { get; init; }
    public required string RootApp { get; init; }
    public required string Tenant { get; init; }
    public RequestMode Mode { get; init; }
}