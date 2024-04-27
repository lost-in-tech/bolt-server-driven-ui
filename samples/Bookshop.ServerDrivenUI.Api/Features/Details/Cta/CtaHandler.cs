using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;

namespace Bookshop.ServerDrivenUI.Api.Features.Details.Cta;

[AutoBind]
public class CtaHandler(IRazorXmlViewParser parser) : ScreenSectionProvider<DetailsRequest>
{
    protected override SectionInfo ForSection => new SectionInfo
    {
        Name = "book-details-cta"
    };
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, DetailsRequest request, CancellationToken ct)
    {
        var elm = await parser.Read(new RazorViewParseRequest<CtaViewModel>
        {
            ViewModel = new CtaViewModel
            {
                Text = "Buy"
            }
        });

        return elm;
    }
}