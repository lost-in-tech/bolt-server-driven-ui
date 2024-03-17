using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Stack : IElement, IHaveElements
{
    public IElement[]? Elements { get; set; }
}