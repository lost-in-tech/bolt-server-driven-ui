using System.Net;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.ServerDrivenUI.Api.Features.List;

public sealed class Endpoint : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGroup("pages")
            .WithName("pages")
            .WithTags("pages")
            .WithOpenApi()
            .MapGet("books/{category?}", List)
            .WithName("List")
            .WithOpenApi();
    }

    [ProducesResponseType<Screen>(statusCode: 200)]
    [ProducesResponseType<string>(statusCode: 500)]
    private Task<IResult> List(IScreenEndpointResultComposer composer, [AsParameters] ListPageRequest request,
        [FromServices]ILogger<Endpoint> logger,
        CancellationToken ct)
    {
        return composer.Compose(request, ct);
    }
}