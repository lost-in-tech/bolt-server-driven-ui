using System;
using Bolt.Sdui.Elements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bolt.Sdui.Serializers.Tests.Fixtures
{
    public class IocFixture
    {
        private IServiceProvider _sp;

        public IocFixture()
        {
            var sc = new ServiceCollection();

            var config = new ConfigurationBuilder().Build();

            SetupFakes(sc, config);

            _sp = sc.BuildServiceProvider();
        }

        private void SetupFakes(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerializers(new SetupSerializersOption
            {
                AssembliesToScan = new[] { this.GetType().Assembly, typeof(Stack).Assembly}
            });
        }

        public IServiceScope CreateScope() => _sp.CreateScope();
    }



    [CollectionDefinition(nameof(IocFixtureCollection), DisableParallelization = false)]
    public class IocFixtureCollection : ICollectionFixture<IocFixture>
    {

    }
}
