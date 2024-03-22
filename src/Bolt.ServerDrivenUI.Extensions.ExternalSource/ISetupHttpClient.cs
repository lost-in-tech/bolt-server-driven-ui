namespace Bolt.ServerDrivenUI.Extensions.ExternalSource;

public interface ISetupHttpClient
{
    void Setup(HttpClient http, SetupHttpClientInput input);
    bool IsApplicable(string serviceName);
}

public record SetupHttpClientInput
{
    public required string ServiceName { get; init; }
    public required string BaseAddress { get; init; }
    public int? TimeoutInMs { get; init; }
}