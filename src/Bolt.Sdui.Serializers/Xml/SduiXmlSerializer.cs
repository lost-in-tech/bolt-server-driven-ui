using Bolt.Sdui.Core;

namespace Bolt.Sdui.Serializers.Xml;

internal sealed class SduiXmlSerializer : ISduiXmlSerializer
{
    private readonly XmlDeserializer _deserializationHelper;
    private readonly XmlSerializer _serializer;

    public SduiXmlSerializer(XmlDeserializer deserializer, XmlSerializer serializer)
    {
        _deserializationHelper = deserializer;
        _serializer = serializer;
    }

    public IElement? Deserialize(Stream source) => _deserializationHelper.Deserialize(source);

    public Stream? Serialize<T>(T? source) => _serializer.Serialize(source);
}
