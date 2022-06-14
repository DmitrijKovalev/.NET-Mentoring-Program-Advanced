using MongoDB.Driver;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.Core.Models.Configuration;

namespace OnlineStore.CartService.DataAccessLayer
{
    /// <summary>
    /// Mongo DB base repository.
    /// </summary>
    public class BaseRepository
    {
        private readonly CartServiceConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public BaseRepository(CartServiceConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (string.IsNullOrEmpty(configuration.DatabaseConnectionString))
            {
                throw new ArgumentNullException($"{nameof(configuration.DatabaseConnectionString)} must be provided.");
            }

            this.configuration = configuration;
        }

        /// <summary>
        /// Gets cart collection name.
        /// </summary>
        /// <value>
        /// <placeholder>Cart collection name.</placeholder>
        /// </value>
        public string CartCollectionName => nameof(Cart);

        /// <summary>
        /// Gets cart collection.
        /// </summary>
        /// <value>
        /// <placeholder>Cart collection.</placeholder>
        /// </value>
        public IMongoCollection<Cart> CartCollection => this.GetContext().GetCollection<Cart>(this.CartCollectionName);

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>Database context.</returns>
        public IMongoDatabase GetContext()
        {
            var connection = new MongoUrlBuilder(this.configuration.DatabaseConnectionString);
            var client = new MongoClient(this.configuration.DatabaseConnectionString);
            var context = client.GetDatabase(connection.DatabaseName);

            return context;
        }
    }
}
