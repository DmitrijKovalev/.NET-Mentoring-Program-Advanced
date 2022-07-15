namespace OnlineStore.CatalogService.Application.Common.Models
{
    /// <summary>
    /// The command response model.
    /// </summary>
    public class CommandResponseModel
    {
        /// <summary>
        /// Gets default command response instance.
        /// </summary>
        /// <value>
        /// <placeholder>Default command response instance.</placeholder>
        /// </value>
        public static CommandResponseModel Default => new CommandResponseModel
        {
            Success = true,
        };

        /// <summary>
        /// Gets or sets a value indicating whether the success flag.
        /// </summary>
        /// <value>
        /// <placeholder>Value that indicates whether the success flag.</placeholder>
        /// </value>
        public bool Success { get; set; }
    }
}
