namespace OnlineStore.CatalogService.WebApi.Models.Hateoas
{
    /// <summary>
    /// Link model.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets href.
        /// </summary>
        /// <value>
        /// <placeholder>Href.</placeholder>
        /// </value>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets rel.
        /// </summary>
        /// <value>
        /// <placeholder>Rel.</placeholder>
        /// </value>
        public string Rel { get; set; }

        /// <summary>
        /// Gets or sets method.
        /// </summary>
        /// <value>
        /// <placeholder>Method.</placeholder>
        /// </value>
        public string Method { get; set; }
    }
}
