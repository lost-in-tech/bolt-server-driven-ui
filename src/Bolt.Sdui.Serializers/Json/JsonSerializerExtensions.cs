using System.Text.Json;
using System.Text.Json.Serialization;
using Bolt.Sdui.Core;

namespace Bolt.Sdui.Serializers.Json;

internal static class JsonSerializerExtensions
{
    public static void ApplyUDLStandard(this JsonSerializerOptions opt, ITypeRegistry typeRegistry)
    {
        opt.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.PropertyNameCaseInsensitive = true;
        opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        opt.Converters.Add(new PolymorphicJsonConverter<IElement>(typeRegistry));
        opt.Converters.Add(new PolymorphicJsonConverter<IMetaData>(typeRegistry));
        opt.Converters.Add(new PolymorphicJsonConverter<IUiAction>(typeRegistry));
    }
}
