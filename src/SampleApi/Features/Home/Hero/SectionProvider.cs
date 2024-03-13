using Bolt.IocScanner.Attributes;
using Bolt.MaySucceed;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;

namespace SampleApi.Features.Home.Hero;

[AutoBind]
[MustSucceed]
internal sealed class SectionProvider(IRazorXmlViewParser parser) : ScreenSectionProvider<HomePageRequest>
{
    public override string ForSection => "hero";

    public override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        var rsp = parser.Read<SectionProvider>(new ());

        return rsp;
    }
}