using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bookshop.ServerDriveUI.Elements;

namespace Bookshop.ServerDrivenUI.Api.Features.Home.Intro;

[MustSucceed]
[AutoBind]
public class IntroElementHandler(IHttpRequestWrapper requestWrapper) : ScreenElementProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "home:intro",
        Scope = SectionScope.Default
    };
    
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        //return HttpFailure.Redirect("/test", false).ToMaySucceedTask<IElement>();
        //return HttpFailure.NotFound().ToMaySucceedTask<IElement>();
        
        return new Text
        {
            Value = $"intro:{requestWrapper.Cookie("test")}"
        }.ToMaySucceedTask();
    }
}

[AutoBind]
public class TestLazy : ScreenElementProvider<HomePageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "test",
        Scope = SectionScope.Lazy
    };
    protected override Task<MaySucceed<IElement>> Get(IRequestContextReader context, HomePageRequest request, CancellationToken ct)
    {
        return new Text
        {
            Value = "hello lazy"
        }.ToMaySucceedTask();
    }
}