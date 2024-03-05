using Bolt.MaySucceed;
using Ensemble.Core;
using Microsoft.AspNetCore.Http;

namespace Ensemble.Web;

internal sealed class RequestDataProvider (
    IHttpContextAccessor httpContextAccessor,
    IRequestKeyNamesProvider requestKeyNamesProvider): IRequestDataProvider
{
    public MaySucceed<RequestData> Get()
    {
        var keys = requestKeyNamesProvider.Get();

        return new RequestData
        {
            App = ReadHeaderValue(keys.App),
            Id = ReadHeaderValue(keys.Id),
            RootApp = ReadHeaderValue(keys.RootApp),
            RootId = ReadHeaderValue(keys.RootId), 
            Device = ReadHeaderAsEnum<Device>(keys.Device), 
            Platform = ReadHeaderAsEnum<Platform>(keys.Platform),
            UserAgent = ReadHeaderValue(DefaultRequestDataKeys.UserAgent),
            LayoutVersionId = ReadHeaderValue(keys.LayoutVersionId),
            SectionNames = ReadQueryAsArray(keys.SectionNames)
        };
    }

    private string[] ReadQueryAsArray(string? key)
    {
        return httpContextAccessor
                .HttpContext
                .Request.Query[key].ToArray();
    }

    private TEnum? ReadHeaderAsEnum<TEnum>(string key) where TEnum : struct
    {
        var value = ReadHeaderValue(key);

        if (string.IsNullOrWhiteSpace(value)) return null;

        return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
    }
    
    private string ReadHeaderValue(string key)
    {
        return httpContextAccessor
                .HttpContext
                .Request
                .Headers.TryGetValue(key, out var value) 
            ? value 
            : string.Empty;
    }
}