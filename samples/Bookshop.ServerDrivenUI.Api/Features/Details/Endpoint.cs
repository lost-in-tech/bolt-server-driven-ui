using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Bookshop.ServerDrivenUI.Api.Features.Home;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.ServerDrivenUI.Api.Features.Details;

public class Endpoint : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGroup("pages")
            .WithName("pages")
            .WithTags("pages")
            .WithOpenApi()
            .MapGet("books/{isbn}", GetBookById)
            .WithName("Details")
            .WithOpenApi();
    }

    [ProducesResponseType<Screen>(statusCode: 200)]
    [ProducesResponseType<string>(statusCode: 500)]
    private Task<IResult> GetBookById(IScreenEndpointResultComposer composer, [AsParameters] DetailsRequest request,
        CancellationToken ct)
    {
        return composer.Compose(request, ct);
    }
}