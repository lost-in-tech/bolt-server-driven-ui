using Bolt.Sdui.Elements;
using Shouldly;

namespace Bolt.Sdui.Serializers.Tests;

public partial class XmlUdlSerializerTests
{
    [Fact]
    public void Serialize_should_work()
    {
        var stack = new Stack();
        stack.Direction = new Responsive<Direction> { Xs = Direction.Vertical, Sm = Direction.Horizontal };
        stack.Elements = new[]
        {
            new Stack
            {
                Elements = new[]
                {
                    new TestTextElement{ Value = "Hello" }
                }
            }
        };

        using var stream = _sut.Serialize(stack);
        stream.ShouldNotBeNull();
        using var reader = new StreamReader(stream);
        reader.ReadToEnd().ShouldContain("Hello");
    }
}