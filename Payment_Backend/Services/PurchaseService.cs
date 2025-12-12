using Payment_Backend.Models;
using Payment_Backend.Models.DTOs;

namespace Payment_Backend.Services;

public class PurchaseService : IPurchaseService
{
    // In-memory storage (replace with database in production)
    private readonly List<Purchase> _purchases = new();
    private readonly IProductService _productService;
    private readonly ISubscriptionService _subscriptionService;

    public PurchaseService(IProductService productService, ISubscriptionService subscriptionService)
    {
        _productService = productService;
        _subscriptionService = subscriptionService;
    }

    public async Task<PurchaseResponse> ValidateAndRecordPurchaseAsync(ValidateReceiptRequest request)
    {
        try
        {
            // Get product details
            var product = await _productService.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                return new PurchaseResponse
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            // Validate receipt based on platform
            bool isValid = request.Platform switch
            {
                Platform.Android => await ValidateGooglePlayReceiptAsync(request.PurchaseToken ?? ""),
                Platform.iOS => await ValidateAppleReceiptAsync(request.Receipt ?? ""),
                _ => false
            };

            if (!isValid)
            {
                return new PurchaseResponse
                {
                    Success = false,
                    Message = "Receipt validation failed"
                };
            }

            // Create purchase record
            var purchase = new Purchase
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                ProductId = request.ProductId,
                TransactionId = request.TransactionId,
                Amount = product.Price,
                Platform = request.Platform,
                Status = PurchaseStatus.Completed,
                PurchaseToken = request.PurchaseToken,
                Receipt = request.Receipt,
                IsAcknowledged = true
            };

            _purchases.Add(purchase);

            // Handle subscription if applicable
            Subscription? subscription = null;
            if (product.Type == ProductType.Subscription)
            {
                subscription = await _subscriptionService.CreateOrUpdateSubscriptionAsync(
                    request.UserId,
                    request.ProductId,
                    purchase.Id,
                    product.SubscriptionPeriod ?? Models.SubscriptionPeriod.Monthly,
                    product.HasFreeTrial,
                    product.FreeTrialDays ?? 0
                );
            }

            return new PurchaseResponse
            {
                Success = true,
                Message = "Purchase validated successfully",
                Purchase = purchase,
                Subscription = subscription
            };
        }
        catch (Exception ex)
        {
            return new PurchaseResponse
            {
                Success = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public Task<List<Purchase>> GetPurchaseHistoryAsync(string userId)
    {
        var userPurchases = _purchases
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.PurchaseDate)
            .ToList();
        
        return Task.FromResult(userPurchases);
    }

    public Task<Purchase?> GetPurchaseByIdAsync(string purchaseId)
    {
        return Task.FromResult(_purchases.FirstOrDefault(p => p.Id == purchaseId));
    }

    public async Task<bool> AcknowledgePurchaseAsync(string purchaseId)
    {
        var purchase = await GetPurchaseByIdAsync(purchaseId);
        if (purchase != null)
        {
            purchase.IsAcknowledged = true;
            return true;
        }
        return false;
    }

    // Mock validation methods (implement actual validation in production)
    private Task<bool> ValidateGooglePlayReceiptAsync(string purchaseToken)
    {
        // TODO: Implement Google Play Developer API validation
        // Use Google.Apis.AndroidPublisher.v3 NuGet package
        return Task.FromResult(!string.IsNullOrEmpty(purchaseToken));
    }

    private Task<bool> ValidateAppleReceiptAsync(string receipt)
    {
        // TODO: Implement Apple App Store receipt validation
        // Call Apple's verifyReceipt endpoint
        return Task.FromResult(!string.IsNullOrEmpty(receipt));
    }
}

