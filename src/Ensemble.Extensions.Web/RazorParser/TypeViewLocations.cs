using System.Collections.Concurrent;

namespace Ensemble.Extensions.Web.RazorParser;

internal static class TypeViewLocations
{
    private static readonly ConcurrentDictionary<string, string> _store = new ConcurrentDictionary<string, string>();

    public static string Get<T>(string? rootFolder = null, string? viewFolder = null)
    {
        var type = typeof(T);
        rootFolder = rootFolder ?? "Features";
        viewFolder = viewFolder ?? "Views";

        return _store.GetOrAdd($"{type.FullName}{rootFolder}{viewFolder}", (key) =>
        {
            var typeNamespace = type.Namespace;

            if (typeNamespace == null) return string.Empty;

            // TODO: make it configurable, as consumer might don't wanna to use features folder
            var featurePositions = typeNamespace.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase);

            var featurePath = featurePositions == -1 ? typeNamespace : typeNamespace.Substring(featurePositions);

            var typeNamespaceParts = new List<string> { "~" };

            typeNamespaceParts.AddRange(featurePath.Split('.', StringSplitOptions.RemoveEmptyEntries));

            typeNamespaceParts.Add(viewFolder);
            typeNamespaceParts.Add("{0}.cshtml");

            var viewPath = string.Join("/", typeNamespaceParts.ToArray());

            return viewPath;
        });
    }
}