using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Stack : IElement, IHaveElements
{
    public IElement[]? Elements { get; set; }
}

public record Paragraph : IElement
{
    public required string Text { get; init; }
}