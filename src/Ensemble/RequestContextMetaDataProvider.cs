using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble.Core;

namespace Ensemble;

internal sealed class RequestContextMetaDataProvider<TRequest> : ScreenMetaDataProvider<TRequest>
{
    public override Task<MaySucceed<IMetaData[]>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        var requestData = context.RequestData();
        return Task.FromResult<MaySucceed<IMetaData[]>>(new []
        {
            new RequestContextMetaData
            {
                Id = requestData.Id,
                RootId = requestData.RootId,
                Tenant = requestData.Tenant,
                App = requestData.App,
                RootApp = requestData.RootApp
            }
        });
    }
}

public record RequestContextMetaData : IMetaData
{
    public required string Id { get; init; }
    public required string RootId { get; init; }
    public required string App { get; init; }
    public required string RootApp { get; init; }
    public required string Tenant { get; init; }
}