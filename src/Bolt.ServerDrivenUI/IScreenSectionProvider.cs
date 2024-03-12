using Bolt.MaySucceed;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IScreenSectionProvider<in TRequest>
{
    string ForSection { get; }
    Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class ScreenSectionProvider<TRequest> : IScreenSectionProvider<TRequest>
{
    public abstract string ForSection { get; }

    public abstract Task<MaySucceed<IElement>> Get(IRequestContextReader context, TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;

    protected Task<MaySucceed<IElement>> ToResponseTask(IElement element) =>
        Task.FromResult(ToResponse(element));
    protected MaySucceed<IElement> ToResponse(IElement element) =>
        MaySucceed<IElement>.Ok(element);

    private static readonly IElement EmptyElement = new EmptyElement();
    protected Task<MaySucceed<IElement>> NoneAsTask() => ToResponseTask(EmptyElement);
    protected MaySucceed<IElement> None() => ToResponse(EmptyElement);
}