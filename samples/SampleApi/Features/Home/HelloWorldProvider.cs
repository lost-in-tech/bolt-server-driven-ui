using Bolt.IocScanner.Attributes;
using Bolt.Endeavor;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Core;
using Sample.Elements;

namespace SampleApi.Features.Home;

[AutoBind]
internal sealed class HelloWorldProvider : ScreenSectionProvider<HomePageRequest>
{
    public override string ForSection => "hello-world";

    public override Task<Bolt.Endeavor.MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello world"
        }.ToMaySucceedTask();
    }
}

[AutoBind]
internal sealed class HelloJupiterProvider : ScreenSectionProvider<HomePageRequest>
{
    public override string ForSection =>  "hello-jupiter";
    public override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Paragraph
        {
            Text = "Hello Jupiter"
        }.ToMaySucceedTask();
    }
}
