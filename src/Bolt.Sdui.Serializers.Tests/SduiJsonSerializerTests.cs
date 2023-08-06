using Bolt.Sdui.Serializers.Tests.Fixtures;

namespace Bolt.Sdui.Serializers.Tests;
public partial class SduiJsonSerializerTests : TestWithIocFixture
{
    private readonly ISduiJsonSerializer _sut;

    public SduiJsonSerializerTests(IocFixture fixture) : base(fixture)
    {
        _sut = GetRequiredService<ISduiJsonSerializer>();    
    }
}
