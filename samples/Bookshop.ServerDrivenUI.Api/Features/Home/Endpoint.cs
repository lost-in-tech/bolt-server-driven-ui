using System.Net;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.ServerDrivenUI.Api.Features.Home;

public sealed class Endpoint : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGroup("pages")
            .WithName("pages")
            .WithTags("pages")
            .WithOpenApi()
            .MapGet("home", GetBookById)
            .WithName("Home")
            .WithOpenApi();
    }

    [ProducesResponseType<Screen>(statusCode: 200)]
    [ProducesResponseType<string>(statusCode: 500)]
    private Task<IResult> GetBookById(IScreenEndpointResultComposer composer, [AsParameters] HomePageRequest request,
        CancellationToken ct)
    {
        return composer.Compose(request, ct);
    }
}

public record HomePageRequest
{
}