namespace Ensemble.Core;

public static class RequestContextExtensions
{
    public static T? TryGet<T>(this IRequestContextReader context) => context.TryGet<T>(GetKey<T>());
    public static T Get<T>(this IRequestContextReader context, T defaultValue) => context.TryGet<T>(GetKey<T>()) ?? defaultValue;

    public static void Set<T>(this IRequestContextWriter context, T value) =>
        context.Set(GetKey<T>(), value);

    public static RequestData RequestData(this IRequestContextReader reader) =>
        reader.Get<RequestData>(new RequestData
        {
            App = string.Empty,
            Id = string.Empty,
            RootApp = string.Empty,
            RootId = string.Empty
        }); 
    
    private static string GetKey<T>()
    {
        var type = typeof(T);

        return type.FullName ?? type.Name;
    }
}