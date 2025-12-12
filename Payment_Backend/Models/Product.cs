namespace Payment_Backend.Models;

public enum ProductType
{
    Subscription,
    Consumable,
    NonConsumable
}

public enum SubscriptionPeriod
{
    Monthly,
    Yearly
}

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
    public SubscriptionPeriod? SubscriptionPeriod { get; set; }
    public string? IconName { get; set; }
    public string? IconColor { get; set; }
    public List<string> Features { get; set; } = new();
    public bool IsBestValue { get; set; }
    public int? SavePercentage { get; set; }
    public bool HasFreeTrial { get; set; }
    public int? FreeTrialDays { get; set; }
    
    // Platform-specific product IDs
    public string? AndroidProductId { get; set; }
    public string? IosProductId { get; set; }
}

