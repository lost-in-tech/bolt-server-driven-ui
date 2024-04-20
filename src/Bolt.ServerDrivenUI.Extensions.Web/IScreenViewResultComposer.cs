using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public interface IScreenViewResultComposer
{
    public Task<IActionResult> Compose<TRequest>(TRequest request, CancellationToken ct);
}

public interface IScreenEndpointResultComposer
{
    public Task<IResult> Compose<TRequest>(TRequest request, CancellationToken ct);
}