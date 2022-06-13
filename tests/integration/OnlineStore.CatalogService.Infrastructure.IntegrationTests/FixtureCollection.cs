using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace OnlineStore.CatalogService.Infrastructure.IntegrationTests
{
    /// <summary>
    /// Collection defined to share fixture among tests in several test classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(FixtureCollection))]
    public class FixtureCollection : ICollectionFixture<Fixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
