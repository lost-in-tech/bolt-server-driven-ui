using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Extensions.Web;

internal sealed class RequestDataProvider (
    IHttpRequestWrapper requestWrapper,
    IRequestKeyNamesProvider requestKeyNamesProvider): IRequestDataProvider
{
    public Bolt.Endeavor.MaySucceed<RequestData> Get()
    {
        var keys = requestKeyNamesProvider.Get();

        var app = requestWrapper.Header(keys.App);
        var correlationId = EmptyAlternative(
                        requestWrapper.Header(keys.CorrelationId), 
                        EmptyAlternative(requestWrapper.RootTraceId(), Guid.NewGuid().ToString()));

        var sectionNames = ReadQueryAsArray(keys.SectionNames);
        var mode = ReadQueryAsEnum<RequestMode>(keys.Mode);
        mode = mode ?? (sectionNames.Length > 0 ? RequestMode.LazySections : RequestMode.Default);
        
        return new RequestData
        {
            Mode = mode.Value,
            App = app,
            CorrelationId = correlationId,
            RootApp = EmptyAlternative(requestWrapper.Header(keys.RootApp), app),
            Device = ReadHeaderAsEnum<Device>(keys.Device), 
            Platform = ReadHeaderAsEnum<Platform>(keys.Platform),
            UserAgent = requestWrapper.Header(DefaultRequestDataKeys.UserAgent),
            LayoutVersionId = requestWrapper.Header(keys.LayoutVersionId),
            SectionNames = sectionNames,
            Tenant = requestWrapper.Header(keys.Tenant),
            UserId = requestWrapper.UserId(),
            IsAuthenticated = requestWrapper.IsAuthenticated()
        };
    }

    private string EmptyAlternative(string value, string alt)
        => string.IsNullOrWhiteSpace(value) ? alt : value; 

    private const string Separator = ",";
    private string[] ReadQueryAsArray(string? key)
    {
        if (string.IsNullOrWhiteSpace(key)) return Array.Empty<string>();

        var value = requestWrapper.Query(key);

        if (string.IsNullOrWhiteSpace(value)) return Array.Empty<string>();

        return value.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
    }

    private TEnum? ReadQueryAsEnum<TEnum>(string key) where TEnum : struct
    {
        return ReadStringAsEnum<TEnum>(requestWrapper.Query(key));
    }
    
    private TEnum? ReadHeaderAsEnum<TEnum>(string key) where TEnum : struct
    {
        return ReadStringAsEnum<TEnum>(requestWrapper.Header(key));
    }

    private TEnum? ReadStringAsEnum<TEnum>(string value) where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
    }
}