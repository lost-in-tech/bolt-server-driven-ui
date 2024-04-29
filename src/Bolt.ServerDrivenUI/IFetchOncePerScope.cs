using System.Collections.Concurrent;
using Bolt.Endeavor;

namespace Bolt.ServerDrivenUI;

public interface IFetchOncePerScope<TResponse>
{
    Task<MaySucceed<TResponse>> Fetch<T>(string key, 
        T arg, 
        CancellationToken ct, 
        Func<T, CancellationToken, Task<MaySucceed<TResponse>>> fetch);

    Task<MaySucceed<TResponse>> Fetch<T1, T2>(string key,
        T1 arg1,
        T2 arg2,
        CancellationToken ct,
        Func<(T1 Arg1, T2 Arg2), CancellationToken, Task<MaySucceed<TResponse>>> fetch);

    Task<MaySucceed<TResponse>> Fetch<T1, T2, T3>(string key,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        CancellationToken ct,
        Func<(T1 Arg1, T2 Arg2, T3 Arg3), CancellationToken, Task<MaySucceed<TResponse>>> fetch);
}


internal sealed class FetchOncePerScope<TResponse> : IFetchOncePerScope<TResponse>
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _masterLock = new();
    private readonly ConcurrentDictionary<string, MaySucceed<TResponse>> _source = new();
    
    public async Task<MaySucceed<TResponse>> Fetch<T>(string key, 
        T arg, 
        CancellationToken ct, 
        Func<T, CancellationToken, Task<MaySucceed<TResponse>>> fetch)
    {
        if (_source.TryGetValue(key, out var response)) 
            return response;
        
        var semaphoreSlim = _masterLock.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await semaphoreSlim.WaitAsync(ct);

        try
        {
            if (_source.TryGetValue(key, out var existingResponse)) 
                return existingResponse;

            var rsp = await fetch.Invoke(arg, ct);
            
            _source.AddOrUpdate(key, _ => rsp, (_,_) => rsp);
            
            return rsp;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
    
    public Task<MaySucceed<TResponse>> Fetch<T1, T2>(string key, 
        T1 arg1,
        T2 arg2,
        CancellationToken ct, 
        Func<(T1 Arg1, T2 Arg2), CancellationToken, Task<MaySucceed<TResponse>>> fetch)
    {
        return Fetch(key, (arg1, arg2), ct, fetch);
    }
    
    public Task<MaySucceed<TResponse>> Fetch<T1, T2, T3>(string key, 
        T1 arg1,
        T2 arg2,
        T3 arg3,
        CancellationToken ct, 
        Func<(T1 Arg1, T2 Arg2, T3 Arg3), CancellationToken, Task<MaySucceed<TResponse>>> fetch)
    {
        return Fetch(key, (arg1, arg2, arg3), ct, fetch);
    }
}