using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Details.Summary;

[MustSucceed]
[AutoBind]
internal sealed class BookSummaryHandler(IBookRepository repository) : ScreenSectionProvider<DetailsRequest>
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

        return new Stack
        {
            Gap = new Responsive<UiSpace?>()
            {
                Xs = UiSpace.Md
            },
            Elements =
            [
                new Heading
                {
                    Text = book.Title,
                    FontSize = new Responsive<FontSize?>
                    {
                        Xs = FontSize.Xl
                    }
                },
                new Stack()
                {
                    MaxWidth = new Responsive<int?>
                    {
                        Xs = 250
                    },
                    Elements =
                    [
                        new Image()
                        {
                            Url = book.ImageUrl,
                            Alt = book.Title
                        }
                    ]
                },
                new Stack()
                {
                    Elements = [
                        new Paragraph
                        {
                            Text = book.Summary
                        }
                    ]
                },
                new NavigateLink()
                {
                    Url = "/",
                    Elements = new []
                    {
                        new Text
                        {
                            Value = "Back"
                        }
                    }
                }
            ]
        };
    }
}