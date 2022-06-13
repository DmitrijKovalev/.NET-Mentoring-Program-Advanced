namespace OnlineStore.CatalogService.Domain.Common.Exceptions
{
    /// <summary>
    /// Category not found exception.
    /// </summary>
    public class CategoryNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNotFoundException"/> class.
        /// </summary>
        public CategoryNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CategoryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
