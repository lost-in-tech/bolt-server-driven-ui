using Bolt.IocScanner.Attributes;
using Bolt.MaySucceed;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;
using Microsoft.FeatureManagement;
using SampleApi.Elements;

namespace SampleApi.Features.Home;

[AutoBind]
internal sealed class HelloWorldProvider : ScreenSectionProvider<HomePageRequest>
{
    public override string ForSection => "hello-world";

    public override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello world"
        };
    }
}

[AutoBind]
internal sealed class HelloJupiterProvider : ScreenSectionProvider<HomePageRequest>
{
    public override string ForSection =>  "hello-jupiter";
    public override async Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello Jupiter"
        };
    }
}
