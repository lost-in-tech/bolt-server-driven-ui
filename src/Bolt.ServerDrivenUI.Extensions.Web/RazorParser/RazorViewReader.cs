using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Bolt.ServerDrivenUI.Extensions.Web.RazorParser;

public interface IRazorViewReader
{
    Task<string> Read<T>(
        string viewPath,
        T? model,
        CancellationToken cancellationToken = default
    ) where T : class;
}

internal sealed class RazorViewReader(IRazorViewEngine viewEngine,
    ITempDataProvider tempDataProvider,
    IServiceProvider serviceProvider) : IRazorViewReader
{
    private readonly IRazorViewEngine _viewEngine = viewEngine ?? throw new ArgumentNullException(nameof(viewEngine));
    private readonly ITempDataProvider _tempDataProvider = tempDataProvider ?? throw new ArgumentNullException(nameof(tempDataProvider));
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public async Task<string> Read<T>(
        string viewPath,
        T? model,
        CancellationToken cancellationToken = default
    ) where T : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(viewPath))
            throw new ArgumentNullException(nameof(viewPath));
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        DefaultHttpContext defaultHttpContext = new()
        {
            RequestServices = _serviceProvider
        };
        ActionContext actionContext = new(defaultHttpContext, new RouteData(), new ActionDescriptor());

        var view = FindView(actionContext, viewPath);
        
        await using StringWriter stringWriter = new();
        ViewDataDictionary<T> viewDataDictionary = new(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };
        TempDataDictionary tempDataDictionary = new(actionContext.HttpContext, _tempDataProvider);
        ViewContext viewContext = new(
            actionContext,
            view,
            viewDataDictionary,
            tempDataDictionary,
            stringWriter,
            new HtmlHelperOptions()
        );
        await view.RenderAsync(viewContext);

        return stringWriter.ToString();
    }
    
    private IView FindView(ActionContext actionContext, string viewName)
    {
        var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
        if (getViewResult.Success)
        {
            return getViewResult.View;
        }

        var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
        if (findViewResult.Success)
        {
            return findViewResult.View;
        }

        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
        var errorMessage = string.Join(
            Environment.NewLine,
            new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

        throw new InvalidOperationException(errorMessage);
    }
};