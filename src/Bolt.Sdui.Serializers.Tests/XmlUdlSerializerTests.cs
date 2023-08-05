using Bolt.Sdui.Core;
using Bolt.Sdui.Elements;
using Bolt.Sdui.Serializers.Xml;

namespace Bolt.Sdui.Serializers.Tests;

public partial class XmlUdlSerializerTests
{
    private readonly ISduiXmlSerializer _sut;

    public XmlUdlSerializerTests()
    {
        var types = new[] { typeof(IElement), typeof(IMetaData), typeof(IUIAction) };

        var assemblies = new[] { this.GetType().Assembly, typeof(Stack).Assembly };

        var typeRegistry = new TypeRegistry();

        foreach (var assembly in assemblies)
        {
            typeRegistry.Register(assembly, types);
        }

        _sut = new SduiXmlSerializer(new XmlDeserializer(typeRegistry), new XmlSerializer());
    }
}


public record TestStackElement : IElement, IHaveElements
{
    public IElement[]? Elements { get; set; }
}

public record TestBlockElement : IElement, IHaveElements
{
    public IElement[]? Elements { get; set; }
}

public record TestTextElement : IElement
{
    public string? Value { get; init; }
}

public record TestComplexElement : IElement
{
    public string? StringValue { get; init; }
    public int IntValue { get; init; }
    public int? IntNullValue { get; init; }
    public bool BoolValue { get; init; }
    public bool? BoolNullValue { get; init; }
}