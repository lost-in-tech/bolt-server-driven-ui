using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web;

namespace Bookshop.ServerDrivenUI.Api.Features.NotFound;

internal sealed class FallbackScreenComposer<T>(IScreenComposer<NotFoundRequest> composer) : IFallbackScreenComposer<T>
{
    public Task<MaySucceed<Screen>> Compose(T request, Failure failure, CancellationToken ct)
    {
        return composer.Compose(new NotFoundRequest(), ct);
    }

    public bool IsApplicable(T request, Failure failure) => failure.StatusCode == 404;
}