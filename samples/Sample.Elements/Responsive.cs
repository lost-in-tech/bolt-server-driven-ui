namespace Sample.Elements;

public record Responsive<T>
{
    public T? Xs { get; init; }
    public T? Sm { get; init; }
    public T? Md { get; init; }
    public T? Lg { get; init; }
    public T? Xl { get; init; }
}

public enum Gap
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Eight,
    Ten,
    Twelve,
    Fourteen,
    Sixteen,
    Eighteen,
    Twenty,
    TwentyFive,
    Thirty,
    ThirtyFive,
    Forty,
    FortyFive,
    Fifty,
    Sixty,
    Seventy,
    Eighty,
    Ninety,
    Hundred
}