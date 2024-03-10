using Bolt.IocScanner.Attributes;
using Bolt.MaySucceed;
using Bolt.Sdui.Core;
using Ensemble;
using Ensemble.Core;
using SampleApi.Elements;

namespace SampleApi.Features.Home;

[AutoBind]
internal sealed class HelloWorldProvider : SectionElementProvider<HomePageRequest>
{
    protected override string Name => "hello-world";
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello world"
        };
    }
}

[AutoBind]
internal sealed class HelloJupiterProvider : SectionElementProvider<HomePageRequest>
{
    protected override string Name => "hello-jupiter";
    protected override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello Jupiter"
        };
    }
}

[AutoBind]
internal sealed class MetaData : MetaDataProvider<HomePageRequest>
{
    protected override async Task<MaySucceed<IEnumerable<IMetaData>>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new IMetaData[]
        {
            new RequestContextMetaData
            {
                RequestId = context.RequestData().Id
            }
        };
    }
}

public record RequestContextMetaData : IMetaData
{
    public string RequestId { get; init; }
}