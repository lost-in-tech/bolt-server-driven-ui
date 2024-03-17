using Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Tests;

public class HttpRequestUrlBuilderTests
{
    public class Build
    {
        [Fact]
        public void Should_return_current_url_combining_what_provided_and_what_passed()
        {
            var providerOne = Substitute.For<IHttpQueryStringProvider>();
            providerOne.Get().Returns(new (string Key, string? Value)[]
            {
                ("test-1-1","value-1-1"),
                ("test-1-1", null)
            });

            var providerTwo = Substitute.For<IHttpQueryStringProvider>();
            providerTwo.Get().Returns(new (string Key, string? Value)[]
            {
                ("test-2-1","value-2-1"),
                ("test-2-2", "value-2-2")
            });

            var givenQueryStrings = new (string Key, string? Value)[]
            {
                ("test-3-1","value-3-1"),
                ("test-3-2","value-3-2")
            };
            
            var sut = new HttpRequestUrlBuilder(new[] { providerOne, providerTwo});

            var got = sut.Build("/test?name=ruhul", givenQueryStrings);
            var expected = "/test?name=ruhul&test-1-1=value-1-1&test-2-1=value-2-1&test-2-2=value-2-2&test-3-1=value-3-1&test-3-2=value-3-2";
            got.ShouldBe(expected);
        }
    }
}