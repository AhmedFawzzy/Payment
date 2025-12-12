using Payment_Mobile.Models;

namespace Payment_Mobile.Services;

public interface IPaymentService
{
    Task<bool> ConnectAsync();
    Task DisconnectAsync();
    Task<PurchaseResult> PurchaseAsync(Product product);
    Task<bool> RestorePurchasesAsync();
    Task<List<Product>> GetAvailableProductsAsync();
}

