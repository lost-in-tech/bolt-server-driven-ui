using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.ExpensiveContent;

[AutoBind]
internal sealed class CurrentTimeElementHandler : ScreenElementProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new() { Name = "current-time" };
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = $"Current time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
        }.ToMaySucceedTask();
    }
}