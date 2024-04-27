namespace Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<BookRecord>> GetAll();
    Task<BookRecord?> GetById(string isbn);
}

public record BookRecord
{
    public required string Isbn { get; init; }
    public required string Title { get; init; }
    public required string ImageUrl { get; init; }
    public required string Summary { get; init; }
    public double Price { get; init; }
    public required string[] Authors { get; init; }
}