using Plugin.InAppBilling;
using Payment_Mobile.Models;
using PlatformType = Payment_Mobile.Models.Platform;

namespace Payment_Mobile.Services;

public class PaymentService : IPaymentService
{
    private readonly IInAppBilling _inAppBilling;
    private readonly IApiService _apiService;

    public PaymentService(IApiService apiService)
    {
        _inAppBilling = CrossInAppBilling.Current;
        _apiService = apiService;
    }

    public async Task<bool> ConnectAsync()
    {
        try
        {
            var connected = await _inAppBilling.ConnectAsync();
            return connected;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Connection error: {ex.Message}");
            return false;
        }
    }

    public async Task DisconnectAsync()
    {
        await _inAppBilling.DisconnectAsync();
    }

    public async Task<PurchaseResult> PurchaseAsync(Product product)
    {
        try
        {
            var connected = await ConnectAsync();
            if (!connected)
            {
                return new PurchaseResult
                {
                    Success = false,
                    Message = "Failed to connect to payment service"
                };
            }

            // Get platform-specific product ID
            var productId = DeviceInfo.Platform == DevicePlatform.Android
                ? product.AndroidProductId
                : product.IosProductId;

            if (string.IsNullOrEmpty(productId))
            {
                return new PurchaseResult
                {
                    Success = false,
                    Message = "Product not available on this platform"
                };
            }

            // Determine if it's a subscription
            var itemType = product.Type == ProductType.Subscription
                ? ItemType.Subscription
                : ItemType.InAppPurchase;

            // Make the purchase
            var purchase = await _inAppBilling.PurchaseAsync(productId, itemType);

            if (purchase == null)
            {
                return new PurchaseResult
                {
                    Success = false,
                    Message = "Purchase was cancelled or failed"
                };
            }

            // Create purchase record for validation
            var purchaseRecord = new Purchase
            {
                ProductId = product.Id,
                TransactionId = purchase.Id ?? string.Empty,
                Amount = product.Price,
                PurchaseToken = purchase.PurchaseToken,
                Platform = DeviceInfo.Platform == DevicePlatform.Android ? PlatformType.Android : PlatformType.iOS,
                Status = PurchaseStatus.Completed,
                UserId = "current_user" // Replace with actual user ID
            };

            // Validate with backend
            var isValid = await _apiService.ValidatePurchaseAsync(purchaseRecord);

            if (isValid)
            {
                // Acknowledge/consume the purchase
                if (product.Type == ProductType.Consumable)
                {
                    await _inAppBilling.ConsumePurchaseAsync(productId, purchase.PurchaseToken);
                }
                else
                {
                    await _inAppBilling.FinalizePurchaseAsync(new[] { purchase.PurchaseToken });
                }
            }

            await DisconnectAsync();

            return new PurchaseResult
            {
                Success = isValid,
                Message = isValid ? "Purchase successful!" : "Purchase validation failed",
                Purchase = purchaseRecord,
                TransactionId = purchase.Id
            };
        }
        catch (Exception ex)
        {
            await DisconnectAsync();
            return new PurchaseResult
            {
                Success = false,
                Message = $"Purchase failed: {ex.Message}"
            };
        }
    }

    public async Task<bool> RestorePurchasesAsync()
    {
        try
        {
            var connected = await ConnectAsync();
            if (!connected)
                return false;

            // Restore purchases for subscriptions
            var purchases = await _inAppBilling.GetPurchasesAsync(ItemType.Subscription);
            
            // TODO: Validate restored purchases with backend
            
            await DisconnectAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Restore error: {ex.Message}");
            await DisconnectAsync();
            return false;
        }
    }

    public async Task<List<Product>> GetAvailableProductsAsync()
    {
        // This method fetches products from the backend
        return await _apiService.GetAllProductsAsync();
    }
}

