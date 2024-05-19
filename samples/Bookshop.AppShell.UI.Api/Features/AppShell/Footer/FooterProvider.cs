using Bolt.Common.Extensions;
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
    
    protected override Task<MaySucceed<ScreenElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        var element = new Container
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
                        new Stack
                        {
                            Elements =
                            [
                                new Text
                                {
                                    Value = "Footer text"
                                },
                                new Text
                                {
                                    Value =
                                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In mollis eros leo, vel euismod metus maximus et. Morbi sit amet pretium dolor. Ut pretium tincidunt leo, ut pulvinar mauris congue ac. Mauris ut tortor at lorem sollicitudin tincidunt non eu enim. Suspendisse pulvinar commodo tellus, in tempor lacus. Phasellus id dignissim sapien. Fusce eu dignissim arcu, semper aliquet mi. Suspendisse potenti. Aliquam dui dui, dictum vitae justo sit amet, vulputate suscipit augue. Donec vel lacus dui."
                                }
                            ]
                        }
                    ]
                }
            ]
        };
        
        return MaySucceed.Ok(ScreenElement.New(element)).WrapInTask();
    }
}