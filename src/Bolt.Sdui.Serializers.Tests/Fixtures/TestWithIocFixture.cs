using System;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Xunit;

namespace Bolt.Sdui.Serializers.Tests.Fixtures
{
    [Trait("Category", "Fast")]
    [Collection(nameof(IocFixtureCollection))]
    public class TestWithIocFixture : IDisposable
    {
        private readonly IServiceScope _scope;

        public TestWithIocFixture(IocFixture fixture)
        {
            _scope = fixture.CreateScope();
        }

        public T GetRequiredService<T>() where T: class
            => _scope.ServiceProvider.GetRequiredService<T>();

        public void Dispose()
        {
            _scope?.Dispose();
        }

        public T GetFakeService<T>(bool clearReceivedCalls = true) where T : class
        {
            var rsp = _scope.ServiceProvider.GetRequiredService<T>();
            if (clearReceivedCalls)
            {
                rsp.ClearReceivedCalls();
                rsp.ClearSubstitute();
            }
            return rsp;
        }
    }
}
