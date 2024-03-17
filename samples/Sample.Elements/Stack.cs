using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Stack : IElement, IHaveElements
{
    public Direction? Direction { get; set; }
    public IElement[]? Elements { get; set; }
}

public enum Direction
{
    Horizontal,
    Vertical
}