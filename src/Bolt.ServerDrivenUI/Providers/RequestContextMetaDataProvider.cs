using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Providers;

internal sealed class RequestContextMetaDataProvider<TRequest> : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var requestData = context.RequestData();

        return new RequestContextMetaData
        {
            CorrelationId = requestData.CorrelationId,
            Tenant = requestData.Tenant,
            App = requestData.App,
            RootApp = requestData.RootApp
        }.ToMaySucceedTask();
    }
}

public record RequestContextMetaData : IMetaData
{
    public required string CorrelationId { get; init; }
    public required string App { get; init; }
    public required string RootApp { get; init; }
    public required string Tenant { get; init; }
}