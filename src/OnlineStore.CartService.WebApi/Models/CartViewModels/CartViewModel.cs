using System.ComponentModel.DataAnnotations;

namespace OnlineStore.CartService.WebApi.Models.CartViewModels
{
    /// <summary>
    /// Cart view model.
    /// </summary>
    public class CartViewModel
    {
        /// <summary>
        /// Gets or sets unique identification.
        /// </summary>
        /// <value>
        /// <placeholder>Unique identification.</placeholder>
        /// </value>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets cart items.
        /// </summary>
        /// <value>
        /// <placeholder>Cart items.</placeholder>
        /// </value>
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
    }
}
