using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;

namespace SampleApi.Features.Home.AppShell;

//[AutoBind]
internal class AppShellSectionProvider(IExternalScreenProvider externalScreenProvider) : ExternalSectionProvider<HomePageRequest>
{
    public override string[] ForSections => new[] { "top-nav", "footer", "member-status" };
    protected override Task<MaySucceed<Screen>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return externalScreenProvider.Get(context, new ExternalScreenRequest
        {
            ServiceName = "api-sample2",
            Path = "/pages/app-shell",
        }, ct);
    }
}