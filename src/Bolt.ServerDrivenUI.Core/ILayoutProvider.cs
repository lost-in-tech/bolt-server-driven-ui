﻿using Bolt.Endeavor;
using Bolt.ServerDrivenUI.Core.Elements;

namespace Bolt.ServerDrivenUI.Core;

public interface ILayoutProvider<in TRequest>
{
    Task<MaySucceed<IReadOnlyCollection<LayoutResponse>>> Get(
        IRequestContextReader context, 
        TRequest request, 
        CancellationToken ct);
    
    int Priority { get; }
}

public record LayoutResponse
{
    public required string Name { get; init; }
    public string? VersionId { get; init; }
    public bool? NotModified { get; init; }
    public required IElement Element { get; init; }
}

public abstract class LayoutProvider<TRequest> : ILayoutProvider<TRequest>
{
    public abstract Task<MaySucceed<IReadOnlyCollection<LayoutResponse>>> Get(
        IRequestContextReader context, 
        TRequest request,
        CancellationToken ct);

    public virtual int Priority  => 0;
    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}