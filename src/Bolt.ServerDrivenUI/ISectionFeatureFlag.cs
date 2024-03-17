namespace Bolt.ServerDrivenUI;

public interface ISectionFeatureFlag
{
    Task<bool> IsDisabled(string name);
}

internal sealed class NullSectionFeatureFlag : ISectionFeatureFlag
{
    public Task<bool> IsDisabled(string name)
    {
        return Task.FromResult(false);
    }
}