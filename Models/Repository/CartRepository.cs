using FurnitureEmporium.Models.Entity;
using FurnitureEmporium.Models.Interface;
using System.Data;
using Dapper;

namespace FurnitureEmporium.Models.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDbConnection _dbConnection;

        public CartRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddToCartAsync(Cart cart)
        {
            string sql = @"INSERT INTO Cart (UserId, ProductId, Quantity, CreatedAt) 
                           VALUES (@UserId, @ProductId, @Quantity, @CreatedAt)";
            await _dbConnection.ExecuteAsync(sql, cart);
        }

        public async Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId)
        {
            string sql = @"
                SELECT c.Id, c.UserId, c.ProductId, c.Quantity, c.CreatedAt, 
                       p.Name AS ProductName, p.Price AS ProductPrice, p.ImageUrl AS ProductImageUrl, p.Category AS ProductCategory
                FROM Cart c
                INNER JOIN ProductTable p ON c.ProductId = p.Id
                WHERE c.UserId = @UserId";
            return await _dbConnection.QueryAsync<Cart>(sql, new { UserId = userId });
        }

        public async Task RemoveFromCartAsync(int cartId)
        {
            string sql = "DELETE FROM Cart WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = cartId });
        }

        public async Task ClearCartAsync(int userId)
        {
            string sql = "DELETE FROM Cart WHERE UserId = @UserId";
            await _dbConnection.ExecuteAsync(sql, new { UserId = userId });
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            string sql = "UPDATE Cart SET Quantity = @Quantity WHERE Id = @CartItemId";
            await _dbConnection.ExecuteAsync(sql, new { Quantity = quantity, CartItemId = cartItemId });
        }
    }
}
