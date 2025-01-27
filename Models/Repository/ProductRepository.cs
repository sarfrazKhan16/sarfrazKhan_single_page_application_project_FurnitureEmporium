using System.Data;
using System.Threading.Tasks;
using Dapper;
using FurnitureEmporium.Models.Entity;
using FurnitureEmporium.Models.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace FurnitureEmporium.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                string sql = @"INSERT INTO ProductTable (Name, Price, Description, Category, ImageUrl, CreatedAt)
                               VALUES (@Name, @Price, @Description, @Category, @ImageUrl, @CreatedAt)";
                int rowsAffected = await connection.ExecuteAsync(sql, product);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM ProductTable ORDER BY CreatedAt DESC";
            return await connection.QueryAsync<Product>(sql);
        }

        public async Task AddToCartAsync(Cart cart)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = @"INSERT INTO Cart (UserId, ProductId, Quantity, CreatedAt)
                           VALUES (@UserId, @ProductId, @Quantity, @CreatedAt)";
            await connection.ExecuteAsync(sql, cart);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                string sql = "DELETE FROM ProductTable WHERE Id = @ProductId";
                using var connection = new SqlConnection(_connectionString);
                int rowsAffected = await connection.ExecuteAsync(sql, new { ProductId = productId });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return false;
            }
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            string sql = "SELECT * FROM ProductTable WHERE Id = @ProductId";
            using var _dbConnection = new SqlConnection(_connectionString);
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { ProductId = productId });
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            string sql = @"UPDATE ProductTable
                   SET Name = @Name,
                       Price = @Price,
                       ImageUrl = @ImageUrl,
                       Description = @Description,
                       Category = @Category
                   WHERE Id = @Id";
            using var _dbConnection = new SqlConnection(_connectionString);
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, product);
            return rowsAffected > 0;
        }

    }
}
