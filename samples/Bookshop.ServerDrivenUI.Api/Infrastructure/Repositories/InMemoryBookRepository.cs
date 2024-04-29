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
        },
        
        new()
        {
            Title = "In the Lives of Puppets",
            Isbn = "9781250217448",
            Summary = "In a strange little home built into the branches of a grove of trees, live three robots—fatherly inventor android Giovanni Lawson, a pleasantly sadistic nurse machine, and a small vacuum desperate for love and attention. Victor Lawson, a human, lives there too. They’re a family, hidden and safe.",
            Price = 12.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1699617963i/60784549.jpg",
            Authors = ["T.J. Klune"]
        },
        
        new()
        {
            Title = "The Emperor and the Endless Palace",
            Isbn = "9780778305231",
            Summary = "Across these seemingly unrelated timelines woven together only by the twists and turns of fate, two men are reborn, lifetime after lifetime. Within the treacherous walls of an ancient palace and the boundless forests of the Asian wilderness to the heart-pounding cement floors of underground rave scenes, our lovers are inexplicably drawn to each other, constantly tested by the worlds around them.",
            Price = 14.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1695793248i/146145975.jpg",
            Authors = ["Justinian Huang"]
        },
        
        new()
        {
            Title = "If the Tide Turns",
            Isbn = "9781496747532",
            Summary = "1715, Eastham, Massachusetts: As the daughter of a wealthy family, Maria Brown has a secure future mapped out for her, yet it is not the future she wants. Young, headstrong, and restless, Maria has no desire to marry the aging, mean-spirited John Hallett, regardless of his fortune and her parents’ wishes. As for what Maria does want—only one person has ever even asked her that question.",
            Price = 9.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1688423027i/181037580.jpg",
            Authors = ["Rachel Rueckert"]
        },
        
        new()
        {
            Title = "The Day Tripper",
            Isbn = "9780778369646",
            Summary = "It’s 1995, and Alex Dean has it all: a spot at Cambridge University next year, the love of an amazing woman named Holly and all the time in the world ahead of him. That is until a brutal encounter with a ghost from his past sees him beaten, battered and almost drowning in the Thames.",
            Price = 14.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1699912822i/156347494.jpg",
            Authors = ["James Goodhand"]
        },
        
        new()
        {
            Title = "Ant Story",
            Isbn = "9780063294004",
            Summary = "Insect-extraordinaire Jay Hosler is back, this time exploring how we seek to understand ourselves and the world around us through the eyes of one of our world’s tiniest the ant. Meet Rubi, a tiny ant with a big personality and an even bigger love for stories. Who knew the small world of her colony could be full of unexpected friendships, epic adventures, and death-defying escapes? Follow Rubi on the journey of a lifetime as she uncovers the mystery and wonder of one of the world’s tiniest, mightiest insects.",
            Price = 9.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1697938125i/181110020.jpg",
            Authors = ["Jay Hosler"]
        },
        
        new()
        {
            Title = "Deep Water: The world in the ocean",
            Isbn = "9781760146245",
            Summary = "The ocean has shaped and sustained life on Earth for billions of years. Its waters contain our past, from the deep history of evolutionary time to exploration and colonialism; our present, as a place of solace and pleasure, and as the highway that underpins the global economy; and – as waters heat and sea levels rise ever higher – our future.\n",
            Price = 12.99,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1697311870i/199755602.jpg",
            Authors = ["James Bradley"]
        },
        
        new()
        {
            Title = "The Secret History of Bigfoot",
            Isbn = "9781464216633",
            Summary = "Journalist and writer John O'Connor takes readers on a narrative quest through the American wilds in search of Bigfoot, its myth and meaning. Inhabited by an eccentric cast of characters – reputable men of science and deluded charlatans alike – the book explores the zany and secretive world of \"cryptozoology,\" tracking Bigfoot from the Wild Men of Native American and European lore to Harry and the Hendersons, while examining the forces behind our ever-widening belief in the supernatural.",
            Price = 8.57,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1710781758i/152180034.jpg",
            Authors = ["John O’Connor"]
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