using System.Text.Json;

namespace Bolt.Sdui.Serializers.Json;
internal static class DefaultJsonSerializerOption
{
    private static JsonSerializerOptions? _options = null;

    /// <summary>
    /// Should be set on application start.
    /// Important: not thread safe
    /// </summary>
    /// <param name="options"></param>
    internal static void SetDefault(JsonSerializerOptions? options) { _options = options; }

    public static JsonSerializerOptions? Current => _options;
}