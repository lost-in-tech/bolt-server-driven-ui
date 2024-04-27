using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class NavigateLink : IElement, IHaveElements
{
    public required string Url { get; init; }
    public IElement[]? Elements { get; set; }
}