using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.AppShell;

[AutoBind]
internal sealed class AppShellProvider(IExternalScreenProvider externalScreenProvider) 
    : ExternalSectionProvider<HomePageRequest>
{
    public override SectionInfo[] ForSections(IRequestContextReader context, HomePageRequest request)
        => [new() { Name = "app-shell:header" }, new(){ Name = "app-shell:footer"}];

    protected override Task<MaySucceed<Screen>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return externalScreenProvider.Get(context, new ExternalScreenRequest
        {
            Path = "/app-shell",
            ServiceName = "bookshop-appshell-ui-api",
            ForSections = ForSections(context, request)
        }, ct);
    }
}