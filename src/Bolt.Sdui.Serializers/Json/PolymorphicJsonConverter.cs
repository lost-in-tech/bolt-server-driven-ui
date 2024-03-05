using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ensemble.Core.Serializers.Json;

internal class PolymorphicJsonConverter<T> : JsonConverter<T>
{
    private readonly ITypeRegistry _typeRegistry;

    public PolymorphicJsonConverter(ITypeRegistry typeRegistry)
    {
        _typeRegistry = typeRegistry;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return base.CanConvert(typeToConvert);
    }

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            if (!jsonDocument.RootElement.TryGetProperty("type", out var typeProperty))
            {
                throw new JsonException();
            }

            var typePropertyName = typeProperty.GetString();
            
            if (string.IsNullOrWhiteSpace(typePropertyName) == false)
            {
                var type = _typeRegistry.TryGet(typePropertyName);
                if (type != null)
                {
                    var jsonObject = jsonDocument.RootElement.GetRawText();
                    var result = JsonSerializer.Deserialize(jsonObject, type.Type, options);

                    return result == null ? default : (T)result;
                }
            }

            throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case null:
                JsonSerializer.Serialize(writer, default(T)?.GetType(), options);
                break;
            default:
                {
                    var type = value.GetType();

                    // Create a JsonObject to hold the serialized object
                    var jsonObject = new JsonObject();

                    // Serialize the implementation type and store it in the JsonObject
                    using (var jsonDocument = JsonDocument.Parse(JsonSerializer.Serialize(value, type, options)))
                    {
                        // Add extra property to the JsonObject
                        var typeName = JsonDocument.Parse($"\"{type.Name}\"").RootElement;

                        jsonObject.Add("type", typeName);

                        foreach (var property in jsonDocument.RootElement.EnumerateObject())
                        {
                            jsonObject.Add(property.Name, property.Value);
                        }

                        // Write the JsonObject to the Utf8JsonWriter
                        jsonObject.WriteTo(writer);
                    }

                    break;
                }
        }
    }
}
