using Bolt.ServerDrivenUI.Extensions.Web;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi2.Features.AppShell;

[ApiController]
public class AppShellController : Controller
{
    [HttpGet("pages/app-shell")]
    public Task<IActionResult> Get([FromRoute] AppShellRequest request,
        [FromServices] IScreenViewResultComposer composer, CancellationToken ct)
        => composer.Compose(request, ct);
}

public record AppShellRequest
{
}