using Bolt.ServerDrivenUI.Core.Elements;

namespace Sample.Elements;

public record Heading : IElement
{
    public HeadingLevel? Level { get; init; } 
    public required string Text { get; init; }
}

public enum HeadingLevel
{
    One,
    Two,
    Three,
    Four
}