using Bolt.ServerDrivenUI.Core.Elements;

namespace Bookshop.ServerDriveUI.Elements;

public class PageMetaData : IMetaData
{
    public required string Title { get; init; }
    public required IEnumerable<PageMetaDataItem> MetaData { get; init; }
}

public class PageMetaDataItem
{
    public required string Content { get; init; }
    public required string Name { get; init; }
}