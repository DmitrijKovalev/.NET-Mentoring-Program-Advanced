using Microsoft.EntityFrameworkCore;
using OnlineStore.CatalogService.Domain.Entities;
using System.Reflection;

namespace OnlineStore.CatalogService.Infrastructure.Persistence
{
    /// <summary>
    /// Application data base context.
    /// </summary>
    public class AppDataBaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDataBaseContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets categories database set.
        /// </summary>
        /// <value>
        /// <placeholder>Categories database set.</placeholder>
        /// </value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets products database set.
        /// </summary>
        /// <value>
        /// <placeholder>Products database set.</placeholder>
        /// </value>
        public virtual DbSet<Product> Products { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
