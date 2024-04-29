namespace Bookshop.ServerDrivenUI.Api.Features.List.SearchResult;

public record SearchResultViewModel
{
    public string Heading { get; init; }
    //public SearchItemViewModel[] Items { get; init; } 
    
    public List<SearchItemViewModel[]> ItemsGroup { get; init; }
}

public record SearchItemViewModel
{
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string DetailsUrl { get; init; }
}