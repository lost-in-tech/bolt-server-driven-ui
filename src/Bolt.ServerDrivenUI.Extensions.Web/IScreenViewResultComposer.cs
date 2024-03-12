using Bolt.ServerDrivenUI.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public interface IScreenViewResultComposer
{
    public Task<IActionResult> Compose<TRequest>(TRequest request, CancellationToken ct);
}

internal sealed class ScreenViewResultComposer : IScreenViewResultComposer
{
    private readonly IServiceProvider _serviceProvider;

    public ScreenViewResultComposer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<IActionResult> Compose<TRequest>(TRequest request, CancellationToken ct)
    {
        var composer = _serviceProvider.GetRequiredService<IScreenComposer<TRequest>>();

        var rsp = await composer.Compose(request, ct);

        return new ScreenViewResult(rsp);
    }
}