using Bolt.ServerDrivenUI.Core;

namespace Bolt.ServerDrivenUI.Composer;

internal sealed class LoadScreenContextDataProviderTask(
    IEnumerable<IScreenContextDataProvider> screenContextDataProviders)
{
    public Dictionary<string, object> Execute(IRequestContextReader context, IEnumerable<ScreenSection> sections)
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

        if (context.RequestData().Mode != RequestMode.LazySections)
        {
            var lazySections = new List<string>();
            foreach (var screenSection in sections)
            {
                if (screenSection.IsLazy.HasValue && screenSection.IsLazy.Value)
                {
                    lazySections.Add(screenSection.Name);
                }
            }

            result["lazySections"] = lazySections;
        }

        return result;
    }
}