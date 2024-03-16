using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Sample.Elements;

namespace SampleApi2.Features.AppShell.Footer;

[AutoBind]
internal sealed class FooterSectionProvider : ScreenSectionProvider<AppShellRequest>
{
    public override string ForSection => "footer";
    public override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Paragraph { Text = "footer" }.ToMaySucceedTask();
    }
}