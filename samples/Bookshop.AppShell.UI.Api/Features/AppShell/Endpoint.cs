using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.AppShell.UI.Api.Features.AppShell;

public class Endpoint : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGroup("app-shell")
            .WithName("shell")
            .WithTags("shell")
            .WithOpenApi()
            .MapGet("", Get)
            .WithName("app-shell")
            .WithOpenApi();
    }

    [ProducesResponseType<Screen>(statusCode: 200)]
    [ProducesResponseType<string>(statusCode: 500)]
    private Task<IResult> Get(IScreenEndpointResultComposer composer, [AsParameters] AppShellRequest request,
        CancellationToken ct)
    {
        return composer.Compose(request, ct);
    }
}