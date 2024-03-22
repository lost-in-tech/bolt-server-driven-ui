using Bolt.ServerDrivenUI.Extensions.ExternalSource.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public static class IocSetup
{
    public static IServiceCollection AddServerDrivenUiExternalSource(this IServiceCollection source,
        IConfiguration configuration,
        ServerDrivenExternalSourceOption? option = null)
    {
        option ??= new ServerDrivenExternalSourceOption();

        SetupHttpClient(source, configuration, option);

        source.TryAddSingleton<IHttpClientWrap, HttpClientWrap>();
        source.TryAddEnumerable(ServiceDescriptor.Transient<IExternalScreenProvider, ExternalScreenProvider>());

        source.TryAdd(ServiceDescriptor.Transient<IHttpRequestUrlBuilder, HttpRequestUrlBuilder>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpRequestHeadersProvider, HttpRequestHeadersProvider>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpQueryStringProvider, HttpQueryStringProvider>());
        source.TryAddEnumerable(ServiceDescriptor.Transient<IHttpRequestMessageBuilder, HttpRequestMessageBuilder>());

        return source;
    }

    private static void SetupHttpClient(IServiceCollection source, 
        IConfiguration configuration, 
        ServerDrivenExternalSourceOption option)
    {
        var settings = new Dictionary<string, ExternalServiceSettingDto>();
        configuration.GetSection(option.ExternalServiceSettingsConfigName).Bind(settings);

        foreach (var item in settings)
        {
            source.AddHttpClient(item.Key, (sp, http) =>
            {
                SetupHttpClient(item.Key, item.Value, sp, http);
            });
        }
    }

    private static void SetupHttpClient(string serviceName, 
        ExternalServiceSettingDto settings, 
        IServiceProvider sp,
        HttpClient http)
    {
        http.BaseAddress = new Uri(settings.BaseAddress);
                
        var timeoutInMs = settings.TimeoutInMs ?? 0;
                
        if (timeoutInMs > 0)
        {
            http.Timeout = TimeSpan.FromMilliseconds(timeoutInMs);
        }
                
        var setups = sp.GetServices<ISetupHttpClient>();
        var applied = setups.FirstOrDefault(x => x.IsApplicable(serviceName));

        if (applied == null) return;

        applied.Setup(http, new SetupHttpClientInput
        {
            BaseAddress = settings.BaseAddress,
            ServiceName = serviceName,
            TimeoutInMs = settings.TimeoutInMs
        });
    }
}

public record ServerDrivenExternalSourceOption
{
    public string ExternalServiceSettingsConfigName { get; init; } = "Bolt:ServerDrivenUI:ExternalServices";
}

public record ExternalServiceSettingDto
{
    public required string BaseAddress { get; init; }
    public int? TimeoutInMs { get; init; }
}