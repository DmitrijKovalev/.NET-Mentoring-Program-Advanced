using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using Microsoft.Extensions.Configuration;
using OnlineStore.CartService.WebApi.Tests.Integration.Common;
using Xunit;
using TestsConfiguration = OnlineStore.CartService.WebApi.Tests.Integration.Common.Configuration;

namespace OnlineStore.CartService.WebApi.Tests.Integration.TestsFixture
{
    public class Fixture : IAsyncLifetime
    {
        private const string ConfigurationFileName = "appsettings.json";

        public Fixture()
        {
            var options = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(ConfigurationFileName)
               .Build();

            this.Configuration = options.Get<TestsConfiguration>();

            if (this.Configuration.UseDocker)
            {
                this.Container = new TestcontainersBuilder<TestcontainersContainer>()
                    .WithImage("mongo:latest")
                    .WithName("mongo_integration_test")
                    .WithPortBinding(27017)
                    .Build();
            }

            this.HttpClient = new CartServiceWebApiFactory(this.Configuration).CreateClient();
        }

        internal IDockerContainer Container { get; }

        internal TestsConfiguration Configuration { get; }

        internal HttpClient HttpClient { get; }

        public async Task InitializeAsync()
        {
            if (this.Configuration.UseDocker)
            {
                await this.Container.StartAsync();
            }
        }

        public async Task DisposeAsync()
        {
            if (this.Configuration.UseDocker)
            {
                await this.Container.DisposeAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
