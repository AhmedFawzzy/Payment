using Payment_Backend.Models;
using Payment_Backend.Models.DTOs;

namespace Payment_Backend.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> ValidateAndRecordPurchaseAsync(ValidateReceiptRequest request);
    Task<List<Purchase>> GetPurchaseHistoryAsync(string userId);
    Task<Purchase?> GetPurchaseByIdAsync(string purchaseId);
    Task<bool> AcknowledgePurchaseAsync(string purchaseId);
}

