using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Heading : IElement
{
    public Responsive<FontSize?>? FontSize { get; set; }
    public required string Text { get; set; }
}

public enum FontSize
{
    TwoXs, // 11
    Xs, // 12
    Sm, // 14
    Md, // 16
    Lg, // 18
    Xl, // 20
    TwoXl, // 22
    ThreeXl, // 24
    FourXl, // 26
    FiveXl, // 28
    SixXl, // 30
    SevenXl, // 32
    EightXl, // 34
    NineXl, // 36
    TenXl, // 38
    ElevenXl, // 40
}