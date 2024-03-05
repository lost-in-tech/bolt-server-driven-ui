using Ensemble.Core.Serializers.Tests.Fixtures;

namespace Ensemble.Core.Serializers.Tests;
public partial class SduiJsonSerializerTests : TestWithIocFixture
{
    private readonly ISduiJsonSerializer _sut;

    public SduiJsonSerializerTests(IocFixture fixture) : base(fixture)
    {
        _sut = GetRequiredService<ISduiJsonSerializer>();    
    }
}
