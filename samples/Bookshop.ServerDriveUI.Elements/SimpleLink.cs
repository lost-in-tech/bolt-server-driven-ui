using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class SimpleLink : IElement
{
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public LinkTarget? Target { get; set; }
    public Responsive<Color?>? TextColor { get; set; }
    public Responsive<FontSize?>? FontSize { get; set; }
}

public enum LinkTarget
{
    Self,
    Window
}