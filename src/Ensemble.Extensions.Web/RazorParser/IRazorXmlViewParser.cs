using Bolt.MaySucceed;
using Bolt.Polymorphic.Serializer.Xml;
using Bolt.Sdui.Core;

namespace Ensemble.Extensions.Web.RazorParser;

public interface IRazorXmlViewParser
{
    Task<MaySucceed<IElement>> Read(RazorViewParseRequest parseRequest);
    Task<MaySucceed<IElement>> Read<TCaller>(RazorViewParseRequest<TCaller> parseRequest);
}


public record RazorViewParseRequest<T>
{
    public string? ViewName { get; init; }
    public object? ViewModel { get; init; }
    public string? ViewFolder { get; init; } = "Views";
    public string? RootFolder { get; init; } = "Features";
}

public record RazorViewParseRequest
{
    public required string ViewPath { get; init; }
    public object? ViewModel { get; init; }
}

internal sealed class RazorXmlViewParser(RazorViewRenderer viewRenderer, IXmlSerializer xmlSerializer)
    : IRazorXmlViewParser
{
    public async Task<MaySucceed<IElement>> Read(RazorViewParseRequest parseRequest)
    {
        var view = await RenderView(parseRequest);

        var result = xmlSerializer.Deserialize<IElement>(view);

        return result == null 
            ? new Failure(500, "Failed to deserialize razor view to IElement") 
            : MaySucceed<IElement>.Ok(result);
    }

    private async Task<string> RenderView(RazorViewParseRequest parseRequest)
    {
        return await RenderViewFromSource(parseRequest);
    }

    private Task<string> RenderViewFromSource(RazorViewParseRequest parseRequest)
    {
        var path = parseRequest.ViewPath;
        return viewRenderer.RenderAsync(path, parseRequest.ViewModel ?? new object());
    }

    private const string DefaultViewName = "Index";
    public Task<MaySucceed<IElement>> Read<T>(RazorViewParseRequest<T> parseRequest)
    {
        var viewPath = TypeViewLocations.Get<T>(parseRequest.RootFolder, parseRequest.ViewFolder);

        return Read(new RazorViewParseRequest 
        { 
            ViewModel = parseRequest.ViewModel,
            ViewPath = string.Format(viewPath, parseRequest.ViewName ?? DefaultViewName)
        });
    }
}