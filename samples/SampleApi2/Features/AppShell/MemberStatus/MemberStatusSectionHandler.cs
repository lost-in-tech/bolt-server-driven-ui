using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Sample.Elements;

namespace SampleApi2.Features.AppShell.MemberStatus;

[AutoBind]
public class MemberStatusSectionHandler : LazyScreenSectionProvider<AppShellRequest>
{
    protected override string ForSection => "member-status";
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "loading..."
        }.ToMaySucceedTask();
    }

    protected override Task<MaySucceed<IElement>> GetLazy(IRequestContextReader context, AppShellRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Welcome back ruhul (36 unread messages)"
        }.ToMaySucceedTask();
    }
}