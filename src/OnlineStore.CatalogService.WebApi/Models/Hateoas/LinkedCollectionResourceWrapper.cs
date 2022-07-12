namespace OnlineStore.CatalogService.WebApi.Models.Hateoas
{
    /// <summary>
    /// Linked resource wrapper.
    /// </summary>
    /// <typeparam name="T">Type of resource value.</typeparam>
    public class LinkedCollectionResourceWrapper<T> : LinkedResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedCollectionResourceWrapper{T}"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public LinkedCollectionResourceWrapper(IEnumerable<T> value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        /// <value>
        /// <placeholder>Value.</placeholder>
        /// </value>
        public IEnumerable<T> Value { get; set; }
    }
}
