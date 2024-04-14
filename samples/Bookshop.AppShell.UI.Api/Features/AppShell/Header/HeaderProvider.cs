using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.AppShell.UI.Api.Features.AppShell.Header;

[AutoBind]
internal sealed class HeaderProvider: ScreenSectionProvider<AppShellRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "app-shell:header"
    };
    
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Stack
        {
            Direction = new Responsive<Direction?>
            {
                Xs = Direction.Horizontal
            },
            Elements =
            [
                new Text
                {
                    Value = "Header..."
                }
            ]
        }.ToMaySucceedTask();
    }
}