using Payment_Backend.Models;

namespace Payment_Backend.Services;

public interface ISubscriptionService
{
    Task<Subscription?> CreateOrUpdateSubscriptionAsync(
        string userId, 
        string productId, 
        string purchaseId,
        SubscriptionPeriod period,
        bool hasFreeTrial,
        int freeTrialDays);
    
    Task<Subscription?> GetActiveSubscriptionAsync(string userId);
    Task<List<Subscription>> GetSubscriptionHistoryAsync(string userId);
    Task<bool> CancelSubscriptionAsync(string subscriptionId);
}

