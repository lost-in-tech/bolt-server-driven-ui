using Bolt.ServerDrivenUI.Extensions.ExternalSource.Tests.ScenarioBuilders;
using Bolt.ServerDrivenUI.TestHelpers.Extensions;
using Shouldly;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Tests;

public class HttpRequestMessageBuilder_Build_Tests
{
    [Fact]
    public void Should_return_message_that_contains_headers_passed_in_argument()
    {
        var givenInput = new RequestMessageBuilderInput
        {
            Method = HttpMethod.Get,
            Path = "/path?_qs=default",
            Headers = new List<(string Key, string? Value)>
            {
                ("one", "1")
            }
        };
        
        var sut = new TestScenarioForHttpRequestUrlBuilder()
            .WithRequestUrlBuilder(givenInput.Path)
            .WithHeaderProvider(new Dictionary<string, string>()
            {
                ["header-provider-1.1"] = "1.1",
                ["header-provider-1.2"] = "1.2"
            })
            .WithHeaderProvider(new Dictionary<string, string>()
            {
                ["header-provider-2.1"] = "2.1",
                ["header-provider-2.2"] = "2.2"
            }).Build();
        

        var got = sut.Build(givenInput);

        new
        {
            Method = got.Method,
            Headers = got.Headers,
            OriginalString = got.RequestUri?.OriginalString
        }.ShouldMatchApprovedWithDefaultOptions();
    }
}