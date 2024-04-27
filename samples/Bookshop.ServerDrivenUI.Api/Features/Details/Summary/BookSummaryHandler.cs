using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Shared;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Details.Summary;

[MustSucceed]
[AutoBind]
internal sealed class BookSummaryHandler(IBookRepository repository,
    IAppUrlBuilder appUrlBuilder,
    IRazorXmlViewParser viewParser) : ScreenSectionProvider<DetailsRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "book-details"
    };

    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, DetailsRequest request,
        CancellationToken ct)
    {
        var book = await repository.GetById(request.Isbn);

        if (book == null) return HttpFailure.NotFound();

        var vm = new BookSummaryViewModel
        {
            Summary = book.Summary,
            Title = book.Title,
            ImageUrl = book.ImageUrl,
            BackUrl = appUrlBuilder.Home(),
            BackText = "Back to home",
            BackDescription = "Back to home page"
        };

        return await viewParser.Read(new RazorViewParseRequest<BookSummaryViewModel>
        {
            ViewModel = vm
        });
    }
}