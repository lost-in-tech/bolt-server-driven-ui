namespace Bolt.ServerDrivenUI.Core;

public static class DefaultRequestDataKeys
{
    public const string App = "x-app";
    public const string CorrelationId = "x-cid";
    public const string RootApp = "x-root-app";
    public const string Device = "x-device";
    public const string Platform = "x-platform";
    public const string UserAgent = "User-Agent";
    public const string LayoutVersionId = "x-layout-version-id";
    public const string SectionNames = "_sections";
    public const string ScreenSize = "x-screen";
    public const string Tenant = "x-tenant";
    public const string TenantQs = "_tenant";
    public const string Mode = "_mode";
    public const string RootRequestUri = "x-request-uri";
    public const string AuthToken = "x-auth-token";
    public const string Tags = "x-request-tags";
    public const string Lang = "x-lang";
}