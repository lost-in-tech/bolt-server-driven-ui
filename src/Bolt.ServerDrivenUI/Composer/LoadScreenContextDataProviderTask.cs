using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Composer;

internal sealed class LoadScreenContextDataProviderTask(
    IEnumerable<IScreenContextDataProvider> screenContextDataProviders,
    IRequestKeyNamesProvider keyNamesProvider)
{
    public Dictionary<string, object> Execute(IRequestContextReader context, IEnumerable<string> lazySectionNames)
    {
        var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        
        foreach (var dataProvider in screenContextDataProviders)
        {
            var items = dataProvider.Get(context);

            foreach (var item in items)
            {
                if (item.Value != null)
                {
                    result[item.Key] = item.Value;
                }
            }
        }

        if (context.RequestData().Mode == RequestMode.Default)
        {
            var lazySectionsValue = string.Join(",", lazySectionNames);
            
            if (!string.IsNullOrWhiteSpace(lazySectionsValue))
            {
                result["lazySections"] = lazySectionsValue;
                result["lazyRequestPathAndQuery"] = BuildLazyRequestPath(context.RequestData(), lazySectionsValue);
            }
        }

        return result;
    }

    private string BuildLazyRequestPath(RequestData requestData, string lazySections)
    {
        var uri = requestData.RootRequestUri;

        var keySections = keyNamesProvider.Get().SectionNames;
        var encodedLazySectionsValue = Uri.EscapeDataString(lazySections);
        
        if (uri == null) return $"/?{keySections}={encodedLazySectionsValue}";
        
        if(string.IsNullOrWhiteSpace(uri.Query)) return $"/{uri.AbsolutePath.Trim('/')}/?{keySections}={encodedLazySectionsValue}";

        return $"/{uri.PathAndQuery.TrimStart('/')}&{keySections}={encodedLazySectionsValue}";
    }
}