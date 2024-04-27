using Bolt.IocScanner.Attributes;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;

namespace Bookshop.ServerDrivenUI.Api.Infrastructure.Repositories;

[AutoBind]
internal sealed class InMemoryBookRepository : IBookRepository
{
    private static readonly List<BookRecord> Data =
    [
        new()
        {
            Isbn = "9781250280800",
            Title = "Weyward",
            Summary =
                "Under cover of darkness, Kate flees London for ramshackle Weyward Cottage, inherited from a great aunt she barely remembers. With its tumbling ivy and overgrown garden, the cottage is worlds away from the abusive partner who tormented Kate. But she begins to suspect that her great aunt had a secret. One that lurks in the bones of the cottage, hidden ever since the witch-hunts of the 17th century.",
            ImageUrl =
                "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1677756582i/60654349.jpg",
            Price = 19.99,
            Authors = ["Emilia Hart"]
        },

        new()
        {

            Isbn = "9780802162175",
            Title = "The Covenant of Water",
            Summary =
                "Spanning the years 1900 to 1977, The Covenant of Water is set in Kerala, on India’s Malabar Coast, and follows three generations of a family that suffers a peculiar affliction: in every generation, at least one person dies by drowning—and in Kerala, water is everywhere. At the turn of the century, a twelve-year-old girl from Kerala's Christian community, grieving the death of her father, is sent by boat to her wedding, where she will meet her forty-year-old husband for the first time. From this unforgettable new beginning, the young girl—and future matriarch, Big Ammachi—will witness unthinkable changes over the span of her extraordinary life, full of joy and triumph as well as hardship and loss, her faith and love the only constants.",
            ImageUrl =
                "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1687600746i/180357146.jpg",
            Price = 9.24,
            Authors = ["Abraham Verghese"]
        }
    ];
    
    public Task<IEnumerable<BookRecord>> GetAll()
    {
        return Task.FromResult(Data.AsEnumerable());
    }

    public Task<BookRecord?> GetById(string isbn)
    {
        return Task.FromResult(Data.FirstOrDefault(x => x.Isbn == isbn));
    }
}