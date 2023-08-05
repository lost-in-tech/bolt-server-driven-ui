using Bolt.Sdui.Core;

namespace Bolt.Sdui.Serializers;
public interface ISduiXmlSerializer
{
    public IElement? Deserialize(Stream source);
    public Stream? Serialize<T>(T? source);
}
