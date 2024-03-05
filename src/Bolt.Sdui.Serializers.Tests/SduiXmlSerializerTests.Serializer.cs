using Ensemble.Core.Elements;
using Shouldly;

namespace Ensemble.Core.Serializers.Tests;

public partial class SduiXmlSerializerTests
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