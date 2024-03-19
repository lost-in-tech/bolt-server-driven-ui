using Bolt.Polymorphic.Serializer;
using Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public static class IocSetup
{
    public static IServiceCollection AddServerDrivenUiExternalSource(this IServiceCollection source)
    {
        source.AddHttpClient();
        source.TryAddEnumerable(ServiceDescriptor.Transient<IExternalScreenProvider, ExternalScreenProvider>());
        
        source.TryAdd(ServiceDescriptor.Transient<IHttpRequestUrlBuilder, HttpRequestUrlBuilder>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpRequestHeadersProvider, HttpRequestHeadersProvider>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpQueryStringProvider, HttpQueryStringProvider>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpRequestMessageBuilder, HttpRequestMessageBuilder>());
        
        return source;
    }
}