using Shouldly;
using System.Text;
using Ensemble.Core.Elements;

namespace Ensemble.Core.Serializers.Tests;

public partial class SduiXmlSerializerTests
{
    [Fact]
    public void Deserialize_should_work()
    {
        var source = """
<Stack>
    <Direction Xs="Vertical" Sm="Horizontal"/>
    <Elements>
        <Stack>
            <Elements>
                <TestTextElement Value="Hello"/>
            </Elements>
        </Stack>
        <Stack>
            <Elements>
                <TestTextElement Value="Hello"/>
                <TestComplexElement StringValue="Hello" IntValue="12" BoolValue="true" BoolNullValue="true"/>
            </Elements>
        </Stack>
        <Stack>
            <Elements>
                <TestTextElement Value="Hello 3"/>
                <TestComplexElement StringValue="Hello3" IntValue="12" BoolValue="true" BoolNullValue="true"/>
                <TestComplexElement StringValue="Hello4" IntValue="16" BoolValue="true"/>
            </Elements>
        </Stack>
    </Elements>
</Stack>
""";
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(source));
        
        var elem = _sut.Deserialize(ms);

        elem.ShouldNotBeNull();

        var stack = (Stack)elem;

        stack.Elements.ShouldNotBeNull();
        stack.Elements.Length.ShouldBe(3);
    }
}
