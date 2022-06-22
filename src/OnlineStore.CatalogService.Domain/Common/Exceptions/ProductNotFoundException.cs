namespace OnlineStore.CatalogService.Domain.Common.Exceptions
{
    /// <summary>
    /// Cart not found exception.
    /// </summary>
    public class ProductNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class.
        /// </summary>
        public ProductNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ProductNotFoundException(string message)
            : base(message)
        {
        }
    }
}
