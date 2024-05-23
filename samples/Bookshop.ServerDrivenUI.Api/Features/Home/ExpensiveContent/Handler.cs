using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDriveUI.Elements;
using Bookshop.ServerDriveUI.Elements.Layouts;
using Microsoft.Extensions.Caching.Memory;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.ExpensiveContent;

[AutoBind]
internal sealed class Handler(IMemoryCache memoryCache) : ScreenElementProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new() { Name = "expensive-cache" };
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        if (memoryCache.TryGetValue(ForSection, out var cachedElement))
        {
            if(cachedElement != null) return (MaySucceed<IElement>)cachedElement;
        }
        
        var element = await GetExpensiveElement();

        memoryCache.Set(ForSection, element, DateTimeOffset.Now.AddMinutes(1));

        return element;
    }

    private async Task<MaySucceed<IElement>> GetExpensiveElement()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(500));

        return new Stack
        {
            Gap = new Responsive<UiSpace?>
            {
                Xs  = UiSpace.Md
            },
            Elements =
            [
                new Divider(),
                new Text
                {
                    As = TextAs.H1,
                    Value  = "Non cached Data inside Cached Element Demo",
                    FontSize = new Responsive<FontSize?>
                    {
                        Xs = FontSize.FourXl
                    }
                },
                new Paragraph
                {
                    Text = $"Generated at : {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                },
                new Paragraph
                {
                    Text = "This is dummy expensive content and this should be cached for 1 minute. But you can see the generated time in the middle of this para and next para, update each time you refresh the page."
                },
                new Placeholder
                {
                    Name = "current-time",
                    Sections = ["current-time"]
                },
                new Paragraph
                {
                    Text = "This is last part of the paragraph that started before. But in the middle you should see current time everytime you refresh the page."
                }
            ]
        };
    }
}