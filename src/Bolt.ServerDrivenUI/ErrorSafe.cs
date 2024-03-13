using Bolt.MaySucceed;
using Microsoft.Extensions.Logging;

namespace Bolt.ServerDrivenUI;

internal sealed class ErrorSafe(ILogger<ErrorSafe> logger)
{
    public async Task<MaySucceed<TResponse>> Execute<TResponse>(
        TResponse defaultValue,
        Func<CancellationToken, Task<MaySucceed<TResponse>>> func,
        CancellationToken ct)
    {
        try
        {
            return await func.Invoke(ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        
        return MaySucceed<TResponse>.Ok(defaultValue);
    }
    
    public async Task<MaySucceed<TResponse>> Execute<TRequest, TResponse>(
        TRequest request,
        TResponse defaultValue,
        Func<TRequest, CancellationToken, Task<MaySucceed<TResponse>>> func,
        CancellationToken ct)
    {
        try
        {
            return await func.Invoke(request, ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        
        return MaySucceed<TResponse>.Ok(defaultValue);
    }
    
    public async Task<MaySucceed<TResponse>> Execute<TRequest1, TRequest2, TResponse>(
        TRequest1 request1,
        TRequest2 request2,
        TResponse defaultValue,
        Func<TRequest1, TRequest2, CancellationToken, Task<MaySucceed<TResponse>>> func,
        CancellationToken ct)
    {
        try
        {
            return await func.Invoke(request1, request2, ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
        
        return MaySucceed<TResponse>.Ok(defaultValue);
    }
}