using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Block : IElement, IHaveElements
{
    public Responsive<UiSpace?>? PaddingLeft { get; init; }
    public Responsive<UiSpace?>? PaddingRight { get; init; }
    public Responsive<UiSpace?>? PaddingTop { get; init; }
    public Responsive<UiSpace?>? PaddingBottom { get; init; }
    
    public int? MinWidth { get; init; }
    public int? MaxWidth { get; init; }
    
    public IElement[]? Elements { get; set; }
}