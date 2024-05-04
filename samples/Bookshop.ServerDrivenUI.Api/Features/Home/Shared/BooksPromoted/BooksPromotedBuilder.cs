using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Shared;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.Shared.BooksPromoted;

[AutoBind]
internal sealed class BooksPromotedBuilder(IRazorXmlViewParser parser, IAppUrlBuilder appUrlBuilder)
{
    public Task<MaySucceed<IElement>> Build(BookPromotedInput input)
    {
        var width = input.Books.Length > 2 ? StackWidth.OneFourth : StackWidth.OneHalf;
        return parser.Read(new RazorViewParseRequest<BookPromotedViewModel>
        {
            ViewModel = new BookPromotedViewModel
            {
                IncludeSeparator = input.IncludeSeparator,
                Heading = input.Heading,
                Items = input.Books.Select(x => new BookPromotedItemViewModel
                {
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    DetailsUrl = appUrlBuilder.Details(x.Isbn, x.Title),
                    Width = width
                }).ToArray()
            }
        });
    }
}