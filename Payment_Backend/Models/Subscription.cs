namespace Payment_Backend.Models;

public enum SubscriptionStatus
{
    Active,
    Expired,
    Cancelled,
    GracePeriod,
    Trial
}

public class Subscription
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public SubscriptionStatus Status { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiryDate { get; set; }
    public DateTime? NextBillingDate { get; set; }
    public bool AutoRenew { get; set; } = true;
    public bool IsInTrialPeriod { get; set; }
    public DateTime? TrialEndDate { get; set; }
    public string? CurrentPurchaseId { get; set; }
    public List<string> PurchaseHistory { get; set; } = new();
}

