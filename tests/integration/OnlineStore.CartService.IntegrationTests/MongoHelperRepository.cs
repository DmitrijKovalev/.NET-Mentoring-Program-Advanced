using MongoDB.Driver;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.DataAccessLayer;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.CartService.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class MongoHelperRepository : BaseRepository
    {
        public MongoHelperRepository(CartServiceConfiguration configuration) : base(configuration)
        {
        }

        public FilterDefinition<Cart> EmptyFilter
        {
            get
            {
                var builder = new FilterDefinitionBuilder<Cart>();
                return builder.Empty;
            }
        }
    }
}
