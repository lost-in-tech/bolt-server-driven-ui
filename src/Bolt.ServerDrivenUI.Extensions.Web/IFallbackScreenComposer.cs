using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public interface IFallbackScreenComposer<in TRequest>
{
    Task<MaySucceed<Screen>> Compose(TRequest request, Failure failure, CancellationToken ct);
    bool IsApplicable(TRequest request, Failure failure);
}