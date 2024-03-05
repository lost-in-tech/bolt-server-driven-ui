using Bolt.Sdui.Core;
using Ensemble.Core.Serializers.Tests.Fixtures;
using Ensemble.Core.TestHelpers.Extensions;

namespace Ensemble.Core.Serializers.Tests;

public partial class SduiXmlSerializerTests : TestWithIocFixture
{
    private readonly ISduiXmlSerializer _sut;

    public SduiXmlSerializerTests(IocFixture fixture) : base(fixture)
    {        
        _sut = GetRequiredService<ISduiXmlSerializer>();
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