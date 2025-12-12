using Payment_Backend.Models;

namespace Payment_Backend.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetSubscriptionProductsAsync();
    Task<List<Product>> GetOneTimeProductsAsync();
    Task<Product?> GetProductByIdAsync(string productId);
}

