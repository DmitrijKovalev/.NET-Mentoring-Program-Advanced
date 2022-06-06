using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CartService.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class MongoDatabaseFixture : IAsyncLifetime
    {
        private const string ConfigurationFileName = "appsettings.json";
        private const string DocketImage = "mongo:latest";
        private const string DockerContainerName = "mongo_integration_test";

        public MongoDatabaseFixture()
        {
            var options = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(ConfigurationFileName)
               .Build();

            this.Configuration = options.Get<Configuration>();

            if (this.Configuration.UseDocker)
            {
                this.Container = new TestcontainersBuilder<TestcontainersContainer>()
                    .WithImage(DocketImage)
                    .WithName(DockerContainerName)
                    .WithPortBinding(27017)
                    .Build();
            }
        }

        public IDockerContainer Container { get; }

        public Configuration Configuration { get; }

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
