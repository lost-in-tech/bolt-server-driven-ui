using System.Text.Json;

namespace Bolt.Sdui.Serializers.Json;

internal sealed class JsonObject
{
    private readonly Dictionary<string, JsonElement> _properties;

    public JsonObject()
    {
        _properties = new Dictionary<string, JsonElement>();
    }

    public void Add(string propertyName, JsonElement value)
    {
        _properties[propertyName] = value;
    }

    public void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();

        foreach (var property in _properties)
        {
            writer.WritePropertyName(property.Key);
            property.Value.WriteTo(writer);
        }

        writer.WriteEndObject();
    }
}
