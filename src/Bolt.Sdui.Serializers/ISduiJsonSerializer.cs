namespace Ensemble.Core.Serializers;

public interface ISduiJsonSerializer
{
    string? Serialize<T>(T source);
    T? Deserialize<T>(string source);
    ValueTask<T?> DeserializeAsync<T>(Stream source);
}