namespace Bookshop.ServerDrivenUI.Api.Features.Home.BooksOfTheWeek;

public record BookOfTheWeekViewModel
{
    public string Heading { get; init; }
    public BookOfTheWeekItemViewModel[] Items { get; init; } 
}

public record BookOfTheWeekItemViewModel
{
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string DetailsUrl { get; init; }
}