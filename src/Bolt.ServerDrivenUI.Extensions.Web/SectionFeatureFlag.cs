using Microsoft.Extensions.Options;

namespace Bolt.ServerDrivenUI.Extensions.Web;

public record SectionsFeatureFlagSettings
{
    public Dictionary<string,bool>? Sections { get; init; }
}

internal sealed class SectionFeatureFlag(IOptions<SectionsFeatureFlagSettings> options) : ISectionFeatureFlag
{
    public Task<bool> IsDisabled(string name)
    {
        var settings = options.Value.Sections;
        
        if (settings == null) return Task.FromResult(false);

        if (settings.TryGetValue(name, out var value))
        {
            return Task.FromResult(!value);
        }
        
        return Task.FromResult(false);
    }
}