using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Providers;

public record class LazySections : IMetaData
{
    public required IEnumerable<string> SectionNames { get; init; }
}