using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Application.Common.Models.PaginationModels;

namespace OnlineStore.CatalogService.Application.Common.Extensions
{
    /// <summary>
    /// IQueryble extensions.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// IQueryable object extensions.
        /// </summary>
        /// <typeparam name="TSource">Type of source.</typeparam>
        /// <typeparam name="TDestination">Type of destination.</typeparam>
        /// <param name="source">Queryable source object.</param>
        /// <param name="pagination">Pagination model.</param>
        /// <param name="mapper">Mediatr.</param>
        /// <returns>Paginated result.</returns>
        public static async Task<PaginatedList<TDestination>> ToPaginatedListAsync<TSource, TDestination>(
            this IQueryable<TSource> source,
            Pagination pagination,
            IMapper mapper)
        {
            var itemsSource = source;

            if (pagination is not null)
            {
                itemsSource = itemsSource
                    .Skip(pagination.SkipCount)
                    .Take(pagination.PageSize);
            }

            var items = await itemsSource.ToListAsync();
            var destinitionItems = mapper.Map<IEnumerable<TDestination>>(items);
            var pageNumber = pagination?.PageNumber ?? default;
            var pageSize = pagination?.PageSize ?? default;
            var totalCount = await source.CountAsync();

            return new PaginatedList<TDestination>(destinitionItems, pageNumber, pageSize, totalCount);
        }
    }
}
