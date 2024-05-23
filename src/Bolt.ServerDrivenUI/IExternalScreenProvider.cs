using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.Logging;

namespace Bolt.ServerDrivenUI;

public interface IExternalScreenProvider
{
    Task<MaySucceed<Screen>> Get(IRequestContextReader context, ExternalScreenRequest request, CancellationToken ct);
}

internal sealed class NullExternalScreenProvider(ILogger<NullExternalScreenProvider> logger) : IExternalScreenProvider
{
    public Task<MaySucceed<Screen>> Get(IRequestContextReader context, ExternalScreenRequest request, CancellationToken ct)
    {
        logger.LogError("No external screen provider registered. So all requested will fail. Register an implementation of IExternalScreenProvider");

        return HttpFailure.InternalServerError("No external screen provider registered").ToMaySucceedTask<Screen>();
    }
}