using System.Threading.Tasks;
using FurnitureEmporium.Models.Entity;
using System.Collections.Generic;

namespace FurnitureEmporium.Models.Interface
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task AddToCartAsync(Cart cart);
        Task<bool> DeleteProductAsync(int productId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> UpdateProductAsync(Product product);

    }
}
