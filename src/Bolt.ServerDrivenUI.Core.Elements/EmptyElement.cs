namespace Bolt.ServerDrivenUI.Core.Elements;

public class EmptyElement : IElement
{
    public static IElement Instance { get; } = new EmptyElement();
}