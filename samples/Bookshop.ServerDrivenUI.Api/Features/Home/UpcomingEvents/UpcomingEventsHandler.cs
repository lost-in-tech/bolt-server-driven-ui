using Bolt.Common.Extensions;
using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.UpcomingEvents;

[AutoBind]
public class UpcomingEventsHandler : LazyScreenSectionProvider<HomePageRequest>
{
    protected override Task<MaySucceed<ScreenElement>> Default(IRequestContextReader context, HomePageRequest request)
    {
        var rsp = new LazyBlock
        {
            Section = ForSection,
            Element = new Stack
            {
                Gap = new Responsive<UiSpace?>
                {
                    Xs = UiSpace.Sm
                },
                Elements = [
                    new Divider(),
                    new Text
                    {
                        As = TextAs.H2,
                        Value = "Upcoming events",
                        FontSize = new Responsive<FontSize?>
                        {
                            Xs = FontSize.Xl
                        },
                        FontWeight = new Responsive<FontWeight?>
                        {
                            Xs = FontWeight.Bold
                        }
                    },
                    new Text
                    {
                        As = TextAs.P,
                        Value = "Loading..."
                    }
                ]
            },
        };

        return MaySucceed.Ok(ScreenElement.New(rsp)).WrapInTask();
    }

    protected override async Task<MaySucceed<ScreenElement>> Lazy(IRequestContextReader context, HomePageRequest request)
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        
        var rsp = new Block
        {
            Elements = [new Stack
            {
                Gap = new Responsive<UiSpace?>
                {
                    Xs = UiSpace.Sm
                },
                Direction = new Responsive<Direction?>
                {
                    Xs = Direction.Vertical
                },
                Elements =
                [
                    new Divider(),
                    new Text
                    {
                        As = TextAs.H2,
                        Value = "Upcoming events",
                        FontSize = new Responsive<FontSize?>
                        {
                            Xs = FontSize.Xl
                        },
                        FontWeight = new Responsive<FontWeight?>
                        {
                            Xs = FontWeight.Bold
                        }
                    },
                    new Text
                    {
                        As = TextAs.P,
                        Value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lobortis porta ante, id gravida odio. Quisque malesuada sit amet est nec tristique. Nulla urna velit, iaculis et urna id, pellentesque euismod risus. Duis sollicitudin nunc nec aliquet dapibus. Nam velit est, venenatis sit amet enim sed, sollicitudin auctor turpis. Praesent rutrum risus non urna iaculis condimentum. Vivamus pulvinar mauris et est aliquet, at vehicula risus semper. Donec id molestie sem, et tempor ante. Donec tincidunt augue at nunc mollis sollicitudin. Vestibulum a felis eget massa lobortis finibus id non nisi. Proin eu convallis felis."
                    }
                ]
            }]
        };

        return MaySucceed.Ok(ScreenElement.New(rsp));
    }

    protected override string ForSection => "upcoming-events";
}