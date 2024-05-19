using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IScreenSectionProvider<in TRequest>
{
    SectionInfo[] ForSections(IRequestContextReader context, TRequest request);
    Task<MaySucceed<ScreenSectionResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public struct SectionInfo
{
    public string Name { get; init; }
    public SectionScope Scope { get; init; }

    public static implicit operator SectionInfo(string source) => new()
    {
        Name = source,
        Scope = SectionScope.Default
    };
}

public enum SectionScope
{
    Default, // execute on default request and also when section match on sectionsonly request
    SectionsOnly, // execute when requested section match
    Lazy, // execute on lazy request and section need to match
    Always // execute always
}

public record ScreenSectionResponse
{
    public required IEnumerable<ScreenSection> Sections { get; init; }
    public required IEnumerable<IMetaData> MetaData { get; init; }
}

internal static class SectionInfoExtensions
{
    public static bool IsApplicable(this SectionInfo[] sections, RequestData requestData)
    {
        if (requestData.Mode == RequestMode.Default)
        {
            if (sections.Length == 0) return true;
            
            return sections.Any(x => x.Scope == SectionScope.Default || x.Scope == SectionScope.Always);
        }

        if (requestData.Mode == RequestMode.Sections)
        {
            foreach (var sectionName in requestData.SectionNames)
            {
                if (sections.Any(x => x.Scope == SectionScope.Always
                                    || (string.Equals(x.Name, sectionName, StringComparison.OrdinalIgnoreCase) 
                                        && (x.Scope == SectionScope.Default 
                                            || x.Scope == SectionScope.SectionsOnly)))) return true;
            }
        }

        if (requestData.Mode == RequestMode.LazySections)
        {
            foreach (var sectionName in requestData.SectionNames)
            {
                if (sections.Any(x => x.Scope == SectionScope.Always 
                                      || (string.Equals(x.Name , sectionName, StringComparison.OrdinalIgnoreCase) 
                                          && (x.Scope == SectionScope.Default
                                              || x.Scope == SectionScope.SectionsOnly 
                                              || x.Scope == SectionScope.Lazy)))) return true;
            }
        }
        
        return false;
    }
}