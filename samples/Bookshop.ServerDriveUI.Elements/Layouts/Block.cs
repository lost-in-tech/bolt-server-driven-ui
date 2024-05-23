using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements.Layouts;

public class Block : IElement, IHaveElements
{
    public Responsive<UiSpace?>? PaddingLeft { get; init; }
    public Responsive<UiSpace?>? PaddingRight { get; init; }
    public Responsive<UiSpace?>? PaddingTop { get; init; }
    public Responsive<UiSpace?>? PaddingBottom { get; init; }
    
    public int? MinWidth { get; init; }
    public int? MaxWidth { get; init; }
    
    public int? MaxHeight { get; init; }
    public int? MinHeight { get; init; }
    
    public Responsive<FontSize?>? FontSize { get; set; }
    public Responsive<FontWeight?>? FontWeight { get; set; }
    public Responsive<Color?>? TextColor { get; set; }
    public Responsive<Color?>? BgColor { get; set; }
    
    public IElement[]? Elements { get; set; }
}