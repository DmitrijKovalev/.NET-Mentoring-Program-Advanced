namespace OnlineStore.CartService.Core.Exceptions
{
    /// <summary>
    /// Cart not found exception.
    /// </summary>
    public class CartNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartNotFoundException"/> class.
        /// </summary>
        public CartNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CartNotFoundException(string message)
            : base(message)
        {
        }
    }
}
