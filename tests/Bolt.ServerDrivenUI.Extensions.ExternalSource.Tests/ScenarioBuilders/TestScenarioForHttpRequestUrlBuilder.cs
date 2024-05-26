using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;
using NSubstitute;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource.Tests.ScenarioBuilders;

public class TestScenarioForHttpRequestUrlBuilder
{
    private IHttpRequestUrlBuilder _requestUrlBuilder;
    private IRequestContextReader _requestContextReader;
    private readonly List<IHttpRequestHeadersProvider> _headerProviders = new();

    public TestScenarioForHttpRequestUrlBuilder()
    {
        WithRequestUrlBuilder("/test-path");
        WithRequestContextReader(new RequestData
        {
            App = "test-app",
            Tenant = "test-tenant",
            CorrelationId = "correlation-id",
            RootApp = "root-app",
            Device = Device.Desktop,
            Mode = RequestMode.Default,
            Platform = Platform.Windows,
            IsAuthenticated = false,
            ScreenSize = RequestScreenSize.Wide
        });
    }
    
    public TestScenarioForHttpRequestUrlBuilder WithRequestUrlBuilder(IHttpRequestUrlBuilder requestUrlBuilder)
    {
        _requestUrlBuilder = requestUrlBuilder;
        return this;
    }
    
    public TestScenarioForHttpRequestUrlBuilder WithRequestUrlBuilder(string givenPath)
    {
        var requestUrlBuilder = Substitute.For<IHttpRequestUrlBuilder>();
        requestUrlBuilder.Build(Arg.Any<string>(), Arg.Any<(string, string?)[]>()).Returns(givenPath);
        return WithRequestUrlBuilder(requestUrlBuilder);
    }

    public TestScenarioForHttpRequestUrlBuilder WithRequestContextReader(IRequestContextReader reader)
    {
        _requestContextReader = reader;
        return this;
    }

    public TestScenarioForHttpRequestUrlBuilder WithRequestContextReader(RequestData data)
    {
        var reader = Substitute.For<IRequestContextReader>();
        reader.TryGet<RequestData>(Arg.Any<string>()).Returns(data);
        return WithRequestContextReader(reader);
    }

    public TestScenarioForHttpRequestUrlBuilder WithHeaderProvider(IHttpRequestHeadersProvider provider)
    {
        _headerProviders.Add(provider);
        return this;
    }
    
    public TestScenarioForHttpRequestUrlBuilder WithHeaderProvider(IEnumerable<(string,string)> headers)
    {
        var headerProvider = Substitute.For<IHttpRequestHeadersProvider>();
        headerProvider.Get(Arg.Any<IRequestContextReader>()).Returns(headers);
        
        _headerProviders.Add(headerProvider);
        
        return this;
    }

    public IHttpRequestMessageBuilder Build()
    {
        return new HttpRequestMessageBuilder(_requestUrlBuilder, _headerProviders, _requestContextReader);
    }
}