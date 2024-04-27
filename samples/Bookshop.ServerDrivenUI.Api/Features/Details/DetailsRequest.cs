namespace Bookshop.ServerDrivenUI.Api.Features.Details;

public record DetailsRequest
{
    public required string Isbn { get; init; }
}