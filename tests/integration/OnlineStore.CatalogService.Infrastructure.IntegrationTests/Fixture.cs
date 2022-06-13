using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CatalogService.Infrastructure.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class Fixture : IAsyncLifetime
    {
        private const string ConfigurationFileName = "appsettings.json";

        public Fixture()
        {
            var options = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(ConfigurationFileName)
               .Build();

            this.Configuration = options.Get<Configuration>();

            if (this.Configuration.UseDocker)
            {
                this.Container = new TestcontainersBuilder<TestcontainersContainer>()
                    .WithImage("mcr.microsoft.com/mssql/server:2017-latest")
                    .WithName("ms_sql_integration_test")
                    .WithPortBinding(1433)
                    .WithEnvironment("SA_PASSWORD", "9QW0A0P6rIaB")
                    .WithEnvironment("ACCEPT_EULA", "Y")
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
