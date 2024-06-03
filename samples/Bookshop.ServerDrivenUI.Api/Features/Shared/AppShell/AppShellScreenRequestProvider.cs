using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;

namespace Bookshop.ServerDrivenUI.Api.Features.Shared.AppShell;

internal sealed class AppShellScreenRequestProvider<T> : IExternalScreenRequestProvider<T>
{
    public IEnumerable<ExternalScreenRequest> Get(IRequestContextReader context, T request)
    {
        yield return new ExternalScreenRequest
        {
            Path = "/app-shell",
            ServiceName = "bookshop-appshell-ui-api",
            ForSections = ["app-shell:header" , "app-shell:footer"]
        };
    }
}