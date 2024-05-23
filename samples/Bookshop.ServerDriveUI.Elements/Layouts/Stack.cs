using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements.Layouts;

public class Stack : IElement, IHaveElements
{
    public Responsive<Direction?>? Direction { get; set; }
    public Responsive<StackWidth?>? Width { get; set; }
    public Responsive<int?>? MaxWidth { get; set; }
    public Responsive<int?>? MinWidth { get; set; }
    public Responsive<Height?>? Height { get; set; }
    public Responsive<UiSpace?>? Gap { get; set; }
    public Responsive<AlignX?>? ContentAlignX { get; set; }
    public Responsive<AlignY?>? ContentAlignY { get; set; }
    
    
    public Responsive<FontSize?>? FontSize { get; set; }
    public Responsive<FontWeight?>? FontWeight { get; set; }
    public Responsive<Color?>? TextColor { get; set; }
    public Responsive<Color?>? BgColor { get; set; }
    
    public IElement[]? Elements { get; set; }
}

public enum Height
{
    Full,
    Screen,
    Fit,
}

public enum StackWidth
{
    Full,
    OneHalf,
    OneThird,
    OneFourth,
    OneFifth,
    OneSixth,
    OneSeventh,
    OneEighth,
    OneNinth,
    OneTenth,
}

public enum StackSpace
{
    SixXs, // 2
    FiveXs, // 4
    FourXs, // 6
    ThreeXs, // 8
    TwoXs, // 10
    Xs, // 12
    Sm, // 14
    Md, // 16
    Lg, // 18,
    Xl, // 20,
    TwoXl, // 22,
    ThreeXl, // 24,
    FourXl, // 26,
    FiveXl, // 28,
    SixXl, // 30,
    SevenXl, // 32,
    EightXl, // 34,
    NineXl, // 36,
    TenXl, // 38,
    ElevenXl, // 40,
}

public enum Direction
{
    Vertical,
    Horizontal
}
