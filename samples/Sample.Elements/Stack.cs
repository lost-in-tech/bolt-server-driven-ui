using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Stack : IElement, IHaveElements
{
    public Responsive<Direction?>? Direction { get; set; }
    public Responsive<Gap?>? Gap { get; set; }
    public IElement[]? Elements { get; set; }
}

public enum Direction
{
    Horizontal,
    Vertical
}