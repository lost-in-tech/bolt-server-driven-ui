using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.AppShell.UI.Api.Features.AppShell.Footer;

[AutoBind]
internal sealed class FooterProvider: ScreenSectionProvider<AppShellRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "app-shell:footer"
    };
    
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Container
        {
            Elements =
            [
                new Stack
                {
                    Direction = new Responsive<Direction?>
                    {
                        Xs = Direction.Horizontal
                    },
                    Elements =
                    [
                        new Text
                        {
                            Value = "Footer..."
                        }
                    ]
                }
            ]
        }.ToMaySucceedTask();
    }
}