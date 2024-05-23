using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;

namespace Bookshop.ServerDrivenUI.Api.Features.Details.Cta;

[AutoBind]
public class CtaHandler(IRazorXmlViewParser parser) : ScreenSectionProvider<DetailsRequest>
{
    protected override SectionInfo ForSection => "book-details-cta";

    protected override async Task<MaySucceed<ScreenElement>> Get(
        IRequestContextReader context,
        DetailsRequest request,
        CancellationToken ct)
    {
        var parseRsp = await parser.Read(
            new RazorViewParseRequest<CtaViewModel>
            {
                ViewModel = new CtaViewModel
                {
                    Text = "Buy"
                }
            });

        if (parseRsp.IsFailed) return parseRsp.Failure;

        return ScreenElement.New(parseRsp.Value);
    }
}