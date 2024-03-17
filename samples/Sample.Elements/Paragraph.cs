using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Paragraph : IElement
{
    public required string Text { get; init; }
}