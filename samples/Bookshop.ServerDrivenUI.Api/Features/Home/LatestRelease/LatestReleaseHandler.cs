using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDrivenUI.Api.Features.Home.Shared.BooksPromoted;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.LatestRelease;

[AutoBind]
internal sealed class LatestReleaseHandler(IBookRepository bookRepository,
    BooksPromotedBuilder booksPromotedBuilder) 
    : ScreenSectionProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "latest-release"
    };
    
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        var books = await bookRepository.GetAll();

        return await booksPromotedBuilder.Build(new BookPromotedInput
        {
            Books = books.Skip(2).Take(2).ToArray(),
            Heading = "Latest Releases"
        });
    }
}