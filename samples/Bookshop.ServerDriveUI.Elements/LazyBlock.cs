using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class LazyBlock : IElement
{
    public string? FetchUrl { get; set; }
    public HttpMethod? FetchMethod { get; set; }
    public object? FetchContent { get; set; }
    
    /// <summary>
    /// Section name that this lazy block should render when loading of sections completed
    /// </summary>
    public string? Section { get; set; }
    /// <summary>
    /// Element that you want to render from server side as default
    /// </summary>
    public IElement? Element { get; set; }
    /// <summary>
    /// Element that lazyblock can use to display while loading progressing
    /// </summary>
    public IElement? LoadingElement { get; set; }
}