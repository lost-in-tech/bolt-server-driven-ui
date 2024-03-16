using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Sample.Elements;

namespace SampleApi2.Features.AppShell.TopNav;

[AutoBind]
internal sealed class TopNavSectionProvider : ScreenSectionProvider<AppShellRequest>
{
    public override string ForSection => "top-nav";
    public override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "top-nav"
        }.ToMaySucceedTask();
    }
}