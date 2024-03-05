using System.Text.Json;

namespace Ensemble.Core.Serializers.Json;

public interface IUdlJsonOptionsProvider
{
    JsonSerializerOptions? Get();
}

internal sealed class UdlJsonOptionsProvider : IUdlJsonOptionsProvider
{
    public JsonSerializerOptions? Get() => DefaultJsonSerializerOption.Current;
}