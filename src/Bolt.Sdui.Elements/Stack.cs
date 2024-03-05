using Bolt.Sdui.Core;

namespace Ensemble.Core.Elements;
public record Stack : ElementNode
{
    public Responsive<Direction>? Direction { get; set; }
}
