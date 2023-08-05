namespace Bolt.Sdui.Core;

public interface IHaveElements
{
    IElement[]? Elements { get; set; }
}

/// <summary>
/// Abstract base element that may contain child elements
/// </summary>
public abstract record ElementNode : IElement, IHaveElements
{
    public IElement[]? Elements { get; set; }
}