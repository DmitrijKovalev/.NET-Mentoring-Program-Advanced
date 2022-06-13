using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Dac;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Infrastructure.Persistence;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CatalogService.Infrastructure.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(FixtureCollection))]
    public class EfRepositoryTests : IDisposable
    {
        private const string DacPacPath = @"src\OnlineStore.CatalogService.Database\Snapshots\OnlineStore.CatalogService.Database.dacpac";

        private readonly AppDataBaseFactory dataBaseFactory;

        private bool disposed = false;

        public EfRepositoryTests(Fixture fixture)
        {
            this.dataBaseFactory = new AppDataBaseFactory(fixture.Configuration.DatabaseConnectionString);
            this.PublishDatabase(fixture.Configuration.DatabaseConnectionString);
        }

        [Fact]
        public async Task GivenInsertCategory_WhenCategoryParentIdIsNull_ShouldInsertCategorySuccessfully()
        {
            // Arrange
            var categoryRepository = new EfRepository<Category>(this.dataBaseFactory);
            var category = new Category
            {
                Name = "New Category",
            };

            // Act
            await categoryRepository.InsertAsync(category);
            var insertedCategory = await categoryRepository.GetByIdAsync(category.Id);
            var countOfCategories = categoryRepository.GetAll().ToList().Count;

            // Assert
            insertedCategory.Should().BeEquivalentTo(category);
            countOfCategories.ShouldBe(1);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dataBaseFactory.CreateNewInstance().Database.EnsureDeleted();
                }
            }

            this.disposed = true;
        }

        private void PublishDatabase(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
            var dacpac = DacPackage.Load(@$"{solutiondir}\{DacPacPath}");
            var dacpacService = new DacServices(connectionString);
            dacpacService.Publish(dacpac, connectionStringBuilder.InitialCatalog, new PublishOptions());
        }
    }
}
