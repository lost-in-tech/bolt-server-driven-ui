using Bolt.MaySucceed;
using Ensemble.Core;

namespace Ensemble.Extensions.Web;

internal sealed class RequestDataProvider (
    IHttpRequestWrapper requestWrapper,
    IRequestKeyNamesProvider requestKeyNamesProvider): IRequestDataProvider
{
    public MaySucceed<RequestData> Get()
    {
        var keys = requestKeyNamesProvider.Get();

        var app = requestWrapper.Header(keys.App);
        var id = EmptyAlternative(
                        requestWrapper.Header(keys.Id), 
                        EmptyAlternative(requestWrapper.RootTraceId(), Guid.NewGuid().ToString()));
        
        return new RequestData
        {
            App = app,
            Id = id,
            RootApp = EmptyAlternative(requestWrapper.Header(keys.RootApp), app),
            RootId = EmptyAlternative(requestWrapper.Header(keys.RootId), id), 
            Device = ReadHeaderAsEnum<Device>(keys.Device), 
            Platform = ReadHeaderAsEnum<Platform>(keys.Platform),
            UserAgent = requestWrapper.Header(DefaultRequestDataKeys.UserAgent),
            LayoutVersionId = requestWrapper.Header(keys.LayoutVersionId),
            SectionNames = ReadQueryAsArray(keys.SectionNames),
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

    private TEnum? ReadHeaderAsEnum<TEnum>(string key) where TEnum : struct
    {
        var value = requestWrapper.Header(key);

        if (string.IsNullOrWhiteSpace(value)) return null;

        return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
    }
}