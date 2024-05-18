using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Home.Shared.BooksPromoted;
using Bookshop.ServerDrivenUI.Api.Features.Shared;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.BooksOfTheWeek;

[AutoBind]
internal sealed class BooksOfTheWeekHandler(
    IBookRepository bookRepository,
    BooksPromotedBuilder booksPromotedBuilder) 
    : ScreenElementProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "books-of-the-week"
    };
    
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        var books = await bookRepository.GetAll();

        return await booksPromotedBuilder.Build(new BookPromotedInput
        {
            IncludeSeparator = true,
            Books = books.Take(context.RequestData().ScreenSize == RequestScreenSize.Wide ? 4 : 2).ToArray(),
            Heading = "Books of the week"
        });
    }
}