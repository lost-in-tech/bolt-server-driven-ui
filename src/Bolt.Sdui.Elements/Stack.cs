using Bolt.Sdui.Core;

namespace Bolt.Sdui.Elements;
public record Stack : ElementNode
{
    public Responsive<Direction>? Direction { get; set; }
}
