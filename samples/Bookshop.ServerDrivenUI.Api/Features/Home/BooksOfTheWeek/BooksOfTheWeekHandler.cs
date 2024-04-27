using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.BooksOfTheWeek;

[AutoBind]
internal sealed class BooksOfTheWeekHandler(IRazorXmlViewParser parser, IBookRepository bookRepository) 
    : ScreenSectionProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "books-of-the-week"
    };
    
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        var books = await bookRepository.GetAll();

        return new Stack()
        {
            Gap = new Responsive<UiSpace?>
            {
                Xs = UiSpace.Md
            },
            Elements =
            [
                new Heading
                {
                    Text = "Books of the week",
                    FontSize = new Responsive<FontSize?>
                    {
                        Xs = FontSize.Lg
                    }
                },
                new Stack()
                {
                    Gap = new Responsive<UiSpace?>
                    {
                        Xs = UiSpace.Md
                    },
                    Direction = new Responsive<Direction?>
                    {
                        Xs = Direction.Horizontal
                    },
                    Elements = books.Select(BuildBookElement).ToArray()
                }
            ]
        };
    }

    private IElement BuildBookElement(BookRecord book)
    {
        return new Stack
        {
            Width = new Responsive<StackWidth?>()
            {
                Xs = StackWidth.OneHalf
            },
            Gap = new Responsive<UiSpace?>
            {
                Xs = UiSpace.Sm
            },
            Elements =
            [
                new NavigateLink
                {
                    Url = $"/books/{book.Isbn}",
                    Elements =
                    [
                        new Image
                        {
                            Url = book.ImageUrl,
                            Alt = book.Title
                        }
                    ]
                },
                new NavigateLink
                {
                    Url = $"/books/{book.Isbn}",
                    Elements = [
                        new Text
                        {
                            Value = book.Title
                        }
                    ]
                }
            ]
        };
    }
}