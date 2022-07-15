namespace OnlineStore.CatalogService.WebApi.Models.Hateoas
{
    /// <summary>
    /// Linked resource.
    /// </summary>
    public class LinkedResource
    {
        /// <summary>
        /// Gets or sets links.
        /// </summary>
        /// <value>
        /// <placeholder>Links.</placeholder>
        /// </value>
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
