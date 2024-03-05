using Ensemble.Core.TestHelpers.Extensions;
using Shouldly;
using System.Net;
using Bolt.Sdui.Core;
using Ensemble.Core.Elements;
using Ensemble.Core.Serializers.Tests.Fixtures;

namespace Ensemble.Core.Serializers.Tests;
public partial class SduiJsonSerializerTests : TestWithIocFixture
{
    [Fact]
    public void Serialize_should_work()
    {
        var dto = new SampleDto
        {
            StatusCode = HttpStatusCode.OK,
            Layout = new()
            {
                Wide = new Stack 
                { 
                    Direction = new Responsive<Direction> 
                    { 
                        Sm = Direction.Vertical, 
                        Lg = Direction.Horizontal 
                    },
                    Elements = new IElement[]
                    {
                        new Stack
                        {
                            Elements = new IElement[]
                            {
                                new Placeholder{ Name = "section-1", SectionNames = new []{ "section-1" }}
                            }
                        },
                        new Stack
                        {

                        },
                        new Stack
                        {

                        }
                    }
                }
            },
            Sections = new SectionDto[]
            {
                new()
                {
                    Name = "section-1",
                    Element = new TestTextElement
                    {
                        Value = "hello world"
                    }
                }
            }
        };

        var got = _sut.Serialize( dto );

        got.ShouldNotBeNull();
        got.ShouldMatchApprovedWithDefaultOptions();
    }

    public class SampleDto
    {
        public required HttpStatusCode StatusCode { get; init; }
        public required LayoutDto Layout { get; init; }
        public required SectionDto[] Sections { get; init; }
    }

    public class LayoutDto
    {
        public IElement? Wide { get; init; }
        public IElement? Compact { get; init; }
    }

    public class SectionDto
    {
        public required string Name { get; init; }
        public IElement? Element { get; init; }
    }
}
