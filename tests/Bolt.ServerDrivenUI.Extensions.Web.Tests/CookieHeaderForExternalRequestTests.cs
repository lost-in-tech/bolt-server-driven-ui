using Bolt.ServerDrivenUI.Core;
using NSubstitute;
using Shouldly;

namespace Bolt.ServerDrivenUI.Extensions.Web.Tests;

public class CookieHeaderForExternalRequestTests
{
    private (string Key, string Value)[] ExecuteGet(IEnumerable<(string Key, string Value)> givenCookies)
    {
        var givenRequestWrapper = Substitute.For<IHttpRequestWrapper>();
        givenRequestWrapper.AllCookies().Returns(givenCookies);

        var givenRequestContextReader = Substitute.For<IRequestContextReader>();
        
        var sut = new CookieHeaderForExternalRequest(givenRequestWrapper);

        return sut.Get(givenRequestContextReader).ToArray();
    }
    
    [Fact]
    public void Should_return_no_header_when_request_cookie_is_empty()
    {
        var givenNoCookiesInRequest = Enumerable.Empty<(string, string)>();

        var got = ExecuteGet(givenNoCookiesInRequest);
        
        got.Length.ShouldBe(0);
    }
    
    [Fact]
    public void Should_return_no_header_when_request_has_one_cookie()
    {
        IEnumerable<(string,string)> givenNoCookiesInRequest = [("test", "1")];

        var got = ExecuteGet(givenNoCookiesInRequest);
        
        got.Length.ShouldBe(1);
        got[0].Key.ShouldBe("Cookie");
        got[0].Value.ShouldBe("test=1");
    }
    
    
    
    [Fact]
    public void Should_return_no_header_when_request_has_more_than_one_cookies()
    {
        IEnumerable<(string,string)> givenNoCookiesInRequest = [("test", "1"),("name","ruhul")];

        var got = ExecuteGet(givenNoCookiesInRequest);
        
        got.Length.ShouldBe(1);
        got[0].Key.ShouldBe("Cookie");
        got[0].Value.ShouldBe("test=1; name=ruhul");
    }
}