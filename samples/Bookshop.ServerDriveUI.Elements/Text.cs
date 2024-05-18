using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Text : IElement
{
    public TextAs? As { get; init; }
    public Responsive<FontSize?>? FontSize { get; set; }
    public Responsive<FontWeight?>? FontWeight { get; set; }
    public required string Value { get; set; }
}

public enum FontWeight
{
    Light,
    Regular,
    Medium,
    SemiBold,
    Bold,
    ExtraBold
    
}

public enum FontSize
{
    Xs,
    Sm,
    Md,
    Lg,
    Xl,
    TwoXl,
    ThreeXl,
    FourXl

}

public enum TextAs
{
    P,
    H1,
    H2,
    H3,
    H4,
    H5,
    Strong,
    I
}