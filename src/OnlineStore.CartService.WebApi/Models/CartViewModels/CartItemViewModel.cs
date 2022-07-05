using System.ComponentModel.DataAnnotations;

namespace OnlineStore.CartService.WebApi.Models.CartViewModels
{
    /// <summary>
    /// Cart item view model.
    /// </summary>
    public class CartItemViewModel
    {
        /// <summary>
        /// Gets or sets unique identification.
        /// </summary>
        /// <value>
        /// <placeholder>Unique identification.</placeholder>
        /// </value>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        /// <value>
        /// <placeholder>Name.</placeholder>
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets image URL.
        /// </summary>
        /// <value>
        /// <placeholder>Image URL.</placeholder>
        /// </value>
        [Url]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets price for one item.
        /// </summary>
        /// <value>
        /// <placeholder>Price.</placeholder>
        /// </value>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets items quantity.
        /// </summary>
        /// <value>
        /// <placeholder>Items quantity.</placeholder>
        /// </value>
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
