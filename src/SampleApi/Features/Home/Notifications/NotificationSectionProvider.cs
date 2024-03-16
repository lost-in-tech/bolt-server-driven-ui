using Bolt.IocScanner.Attributes;
using Bolt.Endeavor;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Sample.Elements;

namespace SampleApi.Features.Home.Notifications;

[AutoBind]
internal class NotificationSectionProvider : LazyScreenSectionProvider<HomePageRequest>
{
    protected override string ForSection => "Notifications";
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "loading..."
        }.ToMaySucceedTask();
    }

    protected override Task<MaySucceed<IElement>> GetLazy(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "35 notifications"
        }.ToMaySucceedTask();
    }
}