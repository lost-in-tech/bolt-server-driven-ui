using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolt.Sdui.Serializers.Json;
internal sealed class SduiJsonSerializer : ISduiJsonSerializer
{
    public T? Deserialize<T>(string source)
    {
        if (string.IsNullOrWhiteSpace(source)) return default;

        return JsonSerializer.Deserialize<T>(source, DefaultJsonSerializerOption.Current);
    }

    public ValueTask<T?> DeserializeAsync<T>(Stream source)
    {
        return JsonSerializer.DeserializeAsync<T>(source, DefaultJsonSerializerOption.Current);
    }

    public string? Serialize<T>(T source)
    {
        if (source == null) return default;

        return JsonSerializer.Serialize(source, DefaultJsonSerializerOption.Current);
    }
}
