using Bolt.ServerDrivenUI.Core;
using Microsoft.Extensions.Logging;
using Bolt.Endeavor;

namespace Bolt.ServerDrivenUI.Composer;

internal interface IRequestValidationTask<in TRequest>
{
    Task<MaySucceed> Execute(IRequestContextReader context, TRequest request, CancellationToken ct);
}

internal sealed class RequestValidationTask<TRequest>(IEnumerable<IRequestValidator<TRequest>> validators)
    : IRequestValidationTask<TRequest>
{
    public async Task<MaySucceed> Execute(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        foreach (var requestValidator in validators)
        {
            if (requestValidator.IsApplicable(context, request))
            {
                var rsp = await requestValidator.Validate(context, request, ct);

                if (rsp.IsFailed) return rsp.Failure;
            }
        }

        return MaySucceed.Ok();
    }
}