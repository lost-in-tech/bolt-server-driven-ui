using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Container : IElement, IHaveElements
{
    public Responsive<UiSpace?>? Padding { get; init; }
    public IElement[]? Elements { get; set; }
}