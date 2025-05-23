﻿using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI;

public interface IRequestValidator<in TRequest>
{
    Task<Bolt.Endeavor.MaySucceed> Validate(IRequestContextReader context, TRequest request, CancellationToken ct);
    bool IsApplicable(IRequestContextReader context, TRequest request);
}

public abstract class RequestValidator<TRequest> : IRequestValidator<TRequest>
{
    public abstract Task<Bolt.Endeavor.MaySucceed> Validate(
        IRequestContextReader context, 
        TRequest request,
        CancellationToken ct);

    public virtual bool IsApplicable(IRequestContextReader context, TRequest request) => true;
}