using Payment_Backend.Models;

namespace Payment_Backend.Services;

public class SubscriptionService : ISubscriptionService
{
    // In-memory storage (replace with database in production)
    private readonly List<Subscription> _subscriptions = new();

    public Task<Subscription?> CreateOrUpdateSubscriptionAsync(
        string userId,
        string productId,
        string purchaseId,
        SubscriptionPeriod period,
        bool hasFreeTrial,
        int freeTrialDays)
    {
        // Check if user has existing subscription for this product
        var existingSubscription = _subscriptions
            .FirstOrDefault(s => s.UserId == userId && s.ProductId == productId && s.Status == SubscriptionStatus.Active);

        if (existingSubscription != null)
        {
            // Renew existing subscription
            existingSubscription.NextBillingDate = CalculateNextBillingDate(period);
            existingSubscription.ExpiryDate = existingSubscription.NextBillingDate;
            existingSubscription.PurchaseHistory.Add(purchaseId);
            existingSubscription.CurrentPurchaseId = purchaseId;
            return Task.FromResult<Subscription?>(existingSubscription);
        }

        // Create new subscription
        var subscription = new Subscription
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            ProductId = productId,
            Status = hasFreeTrial ? SubscriptionStatus.Trial : SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow,
            IsInTrialPeriod = hasFreeTrial,
            AutoRenew = true,
            CurrentPurchaseId = purchaseId
        };

        if (hasFreeTrial)
        {
            subscription.TrialEndDate = DateTime.UtcNow.AddDays(freeTrialDays);
            subscription.NextBillingDate = subscription.TrialEndDate;
            subscription.ExpiryDate = subscription.TrialEndDate;
        }
        else
        {
            subscription.NextBillingDate = CalculateNextBillingDate(period);
            subscription.ExpiryDate = subscription.NextBillingDate;
        }

        subscription.PurchaseHistory.Add(purchaseId);
        _subscriptions.Add(subscription);

        return Task.FromResult<Subscription?>(subscription);
    }

    public Task<Subscription?> GetActiveSubscriptionAsync(string userId)
    {
        var activeSubscription = _subscriptions
            .FirstOrDefault(s => s.UserId == userId && 
                (s.Status == SubscriptionStatus.Active || s.Status == SubscriptionStatus.Trial) &&
                s.ExpiryDate > DateTime.UtcNow);

        return Task.FromResult(activeSubscription);
    }

    public Task<List<Subscription>> GetSubscriptionHistoryAsync(string userId)
    {
        var userSubscriptions = _subscriptions
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.StartDate)
            .ToList();

        return Task.FromResult(userSubscriptions);
    }

    public Task<bool> CancelSubscriptionAsync(string subscriptionId)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
        if (subscription != null)
        {
            subscription.AutoRenew = false;
            subscription.Status = SubscriptionStatus.Cancelled;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    private DateTime CalculateNextBillingDate(SubscriptionPeriod period)
    {
        return period switch
        {
            SubscriptionPeriod.Monthly => DateTime.UtcNow.AddMonths(1),
            SubscriptionPeriod.Yearly => DateTime.UtcNow.AddYears(1),
            _ => DateTime.UtcNow.AddMonths(1)
        };
    }
}

