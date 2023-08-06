using Microsoft.Extensions.DependencyInjection;
using NSubstitute.ClearExtensions;
using NSubstitute;
using Bolt.Sdui.TestHelpers.Extensions;

namespace Bolt.Sdui.TestHelpers.Extensions;
public static class ServiceScopeExtensions
{
    public static T GetRequiredService<T>(this IServiceScope scope) where T : class
        => scope.ServiceProvider.GetRequiredService<T>();

    public static T GetFakeService<T>(this IServiceScope scope, bool clearReceivedCalls = true) where T : class
    {
        var rsp = scope.ServiceProvider.GetRequiredService<T>();

        if (clearReceivedCalls)
        {
            rsp.ClearReceivedCalls();
            rsp.ClearSubstitute();
        }

        return rsp;
    }
}
