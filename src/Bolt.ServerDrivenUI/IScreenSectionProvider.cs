using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IScreenSectionProvider<in TRequest>
{
    string[] ForSections { get; }
    Task<MaySucceed<ScreenSectionResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public record ScreenSectionResponse
{
    public required IEnumerable<ScreenSection> Elements { get; init; }
    public required IEnumerable<IMetaData> MetaData { get; init; }
}