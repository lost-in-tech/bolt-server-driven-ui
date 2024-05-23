using Bolt.ServerDrivenUI.Core.Elements;
using Bookshop.ServerDriveUI.Elements.Layouts;

namespace Bookshop.ServerDriveUI.Elements;

public class Divider : IElement
{
    public Direction? Direction { get; set; }
    public DividerVariant? Variant { get; set; }
    public Responsive<DividerWeight?>? Weight { get; set; }
    public Color? Color { get; set; }
}

public enum DividerVariant
{
    Solid,
    Dotted,
    Dashed
}

public enum DividerWeight
{
    Regular,
    Medium,
    Thick,
    ExtraThick
} 