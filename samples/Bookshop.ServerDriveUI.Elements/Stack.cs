using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Stack : IElement, IHaveElements
{
    public Responsive<Direction?>? Direction { get; set; }
    public IElement[]? Elements { get; set; }
}

public class Responsive<T>
{
    public T? Xs { get; set; }
    public T? Sm { get; set; }
    public T? Lg { get; set; }
    public T? Xl { get; set; }
}

public enum Direction
{
    Vertical,
    Horizontal
}