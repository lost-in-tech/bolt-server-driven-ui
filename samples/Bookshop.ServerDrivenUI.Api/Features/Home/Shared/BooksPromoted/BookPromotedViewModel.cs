using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.Shared.BooksPromoted;

public record BookPromotedInput
{
    public bool IncludeSeparator { get; init; }
    public required string Heading { get; init; }
    public required BookRecord[] Books { get; init; }
}

public record BookPromotedViewModel
{
    public bool IncludeSeparator { get; init; }
    public required string Heading { get; init; }
    public required BookPromotedItemViewModel[] Items { get; init; }
}

public record BookPromotedItemViewModel
{
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string DetailsUrl { get; init; }
    public StackWidth Width { get; init; }
}