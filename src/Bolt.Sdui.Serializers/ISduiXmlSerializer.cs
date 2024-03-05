using Bolt.Sdui.Core;

namespace Ensemble.Core.Serializers;
public interface ISduiXmlSerializer
{
    public IElement? Deserialize(Stream source);
    public Stream? Serialize<T>(T? source);
}
