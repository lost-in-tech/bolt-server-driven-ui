using Ensemble.Extensions.Web;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Features.Home;

[ApiController]
public class HomePageController : Controller
{
    [HttpGet("pages/home")]
    public Task<IActionResult> Get([FromRoute] HomePageRequest request,
        [FromServices] IScreenViewResultComposer composer, CancellationToken ct)
        => composer.Compose(request, ct);
}