using Bolt.ServerDrivenUI.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public interface IScreenViewResultComposer
{
    public Task<IActionResult> Compose<TRequest>(TRequest request, CancellationToken ct);
}

public interface IScreenEndpointResultComposer
{
    public Task<IResult> Compose<TRequest>(TRequest request, CancellationToken ct);
}

internal sealed class ScreenViewResultComposer(IServiceProvider serviceProvider) : 
    IScreenViewResultComposer,
    IScreenEndpointResultComposer
{
    async Task<IActionResult> IScreenViewResultComposer.Compose<TRequest>(TRequest request, CancellationToken ct)
    {
        var composer = serviceProvider.GetRequiredService<IScreenComposer<TRequest>>();

        var rsp = await composer.Compose(request, ct);

        return new ScreenViewResult(rsp);
    }

    async Task<IResult> IScreenEndpointResultComposer.Compose<TRequest>(TRequest request, CancellationToken ct)
    {
        var composer = serviceProvider.GetRequiredService<IScreenComposer<TRequest>>();

        var rsp = await composer.Compose(request, ct);

        return new ScreenEndpointResult(rsp);
    }
}