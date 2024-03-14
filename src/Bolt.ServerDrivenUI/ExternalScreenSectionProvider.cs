using Bolt.ServerDrivenUI.Core;
using Bolt.Endeavor;

namespace Bolt.ServerDrivenUI;

public abstract class ExternalScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    public string[] ForSections { get; }
    
    public virtual bool IsLazy(IRequestContextReader context, TRequest request, CancellationToken ct) => false;
    public Task<MaySucceed<ScreenSectionResponse>> Get(IRequestContextReader context, TRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public bool IsApplicable(IRequestContextReader context, TRequest request)
    {
        throw new NotImplementedException();
    }
}

