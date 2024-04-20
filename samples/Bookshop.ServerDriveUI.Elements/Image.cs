using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class Image : IElement
{
    public required string Alt { get; init; }
    public required string Url { get; init; } 
}