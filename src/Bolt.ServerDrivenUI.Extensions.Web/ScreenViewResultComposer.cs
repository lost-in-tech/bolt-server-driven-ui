using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bolt.ServerDrivenUI.Extensions.Web;

internal sealed class ScreenViewResultComposer(IServiceProvider serviceProvider) : 
    IScreenViewResultComposer,
    IScreenEndpointResultComposer
{
    async Task<IActionResult> IScreenViewResultComposer.Compose<TRequest>(TRequest request, CancellationToken ct)
    {
        var rsp = await ComposeScreen(request, ct);
        
        return new ScreenViewResult(rsp);
    }

    async Task<IResult> IScreenEndpointResultComposer.Compose<TRequest>(TRequest request, CancellationToken ct)
    {
        var rsp = await ComposeScreen(request, ct);
        
        return new ScreenEndpointResult(rsp);
    }

    private async Task<WebScreen> ComposeScreen<TRequest>(TRequest request, CancellationToken ct)
    {
        var composer = serviceProvider.GetRequiredService<IScreenComposer<TRequest>>();

        var rsp = await composer.Compose(request, ct);

        if (rsp.IsFailed)
        {
            var fallbackComposers = serviceProvider.GetServices<IFallbackScreenComposer<TRequest>>();

            foreach (var fallbackComposer in fallbackComposers)
            {
                if (!fallbackComposer.IsApplicable(request, rsp.Failure)) continue;

                var fallbackRsp = await fallbackComposer.Compose(request, rsp.Failure, ct);

                if (fallbackRsp.IsSucceed) return BuildWebScreen(fallbackRsp, rsp.Failure);
            }
        }

        return BuildWebScreen(rsp, null);
    }

    private WebScreen BuildWebScreen(MaySucceed<Screen> rsp, Failure? failure)
    {
        return new WebScreen
        {
            Sections = rsp.Value?.Sections ?? [],
            ResponseInstruction = BuildResponseInstruction(rsp, failure),
            ContextData = rsp.Value?.ContextData,
            MetaData = rsp.Value?.MetaData ?? [],
            Layouts = rsp.Value?.Layouts ?? []
        };
    }

    private HttpResponseInstruction BuildResponseInstruction(MaySucceed<Screen> rsp, Failure? failure)
    {
        failure ??= rsp.Failure;
        
        return new HttpResponseInstruction
        {
            HttpStatusCode = EnsureValidHttpStatusCode(failure?.StatusCode ?? rsp.StatusCode),
            RedirectUrl = failure == null
                ? null
                : HttpFailure.TryGetRedirectUrl(failure, out var redirectUrl)
                    ? redirectUrl
                    : null
        };
    }

    private int EnsureValidHttpStatusCode(int statusCode)
    {
        return statusCode is > 100 and < 599
            ? statusCode
            : 200;
    }
}