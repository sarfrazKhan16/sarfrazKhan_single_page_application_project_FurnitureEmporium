using FurnitureEmporium.Models.Entity;

namespace FurnitureEmporium.Models.Interface
{
    public interface ICartRepository
    {
        Task AddToCartAsync(Cart cart);
        Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId);
        Task RemoveFromCartAsync(int cartId);
        Task ClearCartAsync(int userId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    }
}
