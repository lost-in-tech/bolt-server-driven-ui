using Bolt.MaySucceed;
using Ensemble.Core;
using Microsoft.AspNetCore.Http;

namespace Ensemble.Extensions.Web;

internal sealed class RequestDataProvider (
    IHttpContextAccessor httpContextAccessor,
    IRequestKeyNamesProvider requestKeyNamesProvider): IRequestDataProvider
{
    public MaySucceed<RequestData> Get()
    {
        var keys = requestKeyNamesProvider.Get();

        var app = ReadHeaderValue(keys.App);
        var id = EmptyAlternative(ReadHeaderValue(keys.Id), Guid.NewGuid().ToString());
        
        return new RequestData
        {
            App = app,
            Id = id,
            RootApp = EmptyAlternative(ReadHeaderValue(keys.RootApp), app),
            RootId = EmptyAlternative(ReadHeaderValue(keys.RootId), id), 
            Device = ReadHeaderAsEnum<Device>(keys.Device), 
            Platform = ReadHeaderAsEnum<Platform>(keys.Platform),
            UserAgent = ReadHeaderValue(DefaultRequestDataKeys.UserAgent),
            LayoutVersionId = ReadHeaderValue(keys.LayoutVersionId),
            SectionNames = ReadQueryAsArray(keys.SectionNames)
        };
    }

    private string EmptyAlternative(string value, string alt)
        => string.IsNullOrWhiteSpace(value) ? alt : value; 

    private const string Separator = ",";
    private string[] ReadQueryAsArray(string? key)
    {
        if (string.IsNullOrWhiteSpace(key)) return Array.Empty<string>();

        var query = httpContextAccessor.HttpContext?.Request.Query[key];

        if (!query.HasValue || query.Value.Count == 0) return Array.Empty<string>();
        
        return query.Value.ToString().Split(Separator);
    }

    private TEnum? ReadHeaderAsEnum<TEnum>(string key) where TEnum : struct
    {
        var value = ReadHeaderValue(key);

        if (string.IsNullOrWhiteSpace(value)) return null;

        return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
    }
    
    private string ReadHeaderValue(string key)
    {
        var context = httpContextAccessor.HttpContext;

        if (context == null) return string.Empty;
        
        return context
                .Request
                .Headers.TryGetValue(key, out var value) 
            ? value.ToString() 
            : string.Empty;
    }
}