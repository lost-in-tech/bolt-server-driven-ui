using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Shared;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.BooksOfTheWeek;

[AutoBind]
internal sealed class BooksOfTheWeekHandler(IRazorXmlViewParser parser,
    IAppUrlBuilder appUrlBuilder,
    IBookRepository bookRepository) 
    : ScreenSectionProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "books-of-the-week"
    };
    
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        var books = await bookRepository.GetAll();

        return await parser.Read(new RazorViewParseRequest<BookOfTheWeekViewModel>
        {
            ViewModel = new BookOfTheWeekViewModel
            {
                Heading = "Books of the week",
                Items = books.Select(book => new BookOfTheWeekItemViewModel
                {
                    Title = book.Title,
                    ImageUrl = book.ImageUrl,
                    DetailsUrl = appUrlBuilder.Details(book.Isbn, book.Title)
                }).ToArray()
            }
        });
    }
}