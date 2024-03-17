namespace Bolt.ServerDrivenUI.Core;

public static class DefaultRequestDataKeys
{
    public const string App = "x-req-app";
    public const string CorrelationId = "x-req-cid";
    public const string RootApp = "x-req-root-app";
    public const string Device = "x-req-device";
    public const string Platform = "x-req-platform";
    public const string UserAgent = "User-Agent";
    public const string LayoutVersionId = "x-req-layout-version-id";
    public const string SectionNames = "_sections";
    public const string ScreenSize = "x-req-screen";
    public const string Tenant = "x-req-tenant";
    public const string Mode = "_mode";
}