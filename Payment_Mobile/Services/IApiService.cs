using Payment_Mobile.Models;

namespace Payment_Mobile.Services;

public interface IApiService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetSubscriptionProductsAsync();
    Task<List<Product>> GetOneTimeProductsAsync();
    Task<Product?> GetProductByIdAsync(string productId);
    Task<bool> ValidatePurchaseAsync(Purchase purchase);
    Task<List<Purchase>> GetPurchaseHistoryAsync(string userId);
}

