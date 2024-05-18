using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;

namespace Bookshop.AppShell.UI.Api.Features.AppShell.Header;

[AutoBind]
internal sealed class HeaderProvider(IRazorXmlViewParser parser): ScreenElementProvider<AppShellRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "app-shell:header"
    };
    
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return parser.Read(new RazorViewParseRequest<HeaderProvider>
        {
        });
    }
}