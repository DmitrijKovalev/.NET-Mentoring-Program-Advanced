using MongoDB.Driver;
using OnlineStore.CartService.Core.Models;
using OnlineStore.CartService.Core.Models.Configuration;
using OnlineStore.CartService.DataAccessLayer;

namespace OnlineStore.CartService.Tests.Integration.Common
{
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
