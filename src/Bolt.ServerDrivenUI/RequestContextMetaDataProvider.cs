using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

internal sealed class RequestContextMetaDataProvider<TRequest> : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var requestData = context.RequestData();

        return ToResponseTask(new RequestContextMetaData
        {
            CorrelationId = requestData.CorrelationId,
            Tenant = requestData.Tenant,
            App = requestData.App,
            RootApp = requestData.RootApp
        });
    }
}

public record RequestContextMetaData : IMetaData
{
    public required string CorrelationId { get; init; }
    public required string App { get; init; }
    public required string RootApp { get; init; }
    public required string Tenant { get; init; }
}