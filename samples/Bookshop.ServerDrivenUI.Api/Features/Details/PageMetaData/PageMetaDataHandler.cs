using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Details.PageMetaData;

[AutoBind]
internal sealed class PageMetaDataHandler(IBookRepository bookRepository) : ScreenSectionProvider<DetailsRequest>
{
    protected override async Task<MaySucceed<ScreenElement>> Get(IRequestContextReader context, DetailsRequest request,
        CancellationToken ct)
    {
        var book = await bookRepository.GetById(request.Isbn);

        if (book == null) return HttpFailure.NotFound();

        var metaData =
            new ServerDriveUI.Elements.PageMetaData
            {
                Title = book.Title,
                Items = new[]
                {
                    new PageMetaDataItem
                    {
                        Content =
                            "Best books 2023, top books 2023, 2023 Goodreads Choice Awards, votes, ratings, book reviews",
                        Name = "keywords"
                    },
                    new PageMetaDataItem
                    {
                        Content = "Announcing the Goodreads Choice Winner in Best Historical Fiction!",
                        Name = "og:title"
                    }
                }
            };

        return ScreenElement.New(metaData);
    }

    protected override SectionInfo ForSection { get; }
}