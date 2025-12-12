using Payment_Backend.Models;

namespace Payment_Backend.Services;

public class ProductService : IProductService
{
    // In-memory product catalog (replace with database in production)
    private readonly List<Product> _products = new()
    {
        // Subscriptions
        new Product
        {
            Id = "pro_monthly",
            Name = "Monthly Plan",
            Description = "Flexible billing",
            Price = 4.99m,
            Type = ProductType.Subscription,
            SubscriptionPeriod = Models.SubscriptionPeriod.Monthly,
            IconName = "calendar_month",
            IconColor = "slate",
            Features = new() { "Flexible billing", "Cancel anytime" },
            AndroidProductId = "com.payment.pro.monthly",
            IosProductId = "com_payment_pro_monthly"
        },
        new Product
        {
            Id = "pro_yearly",
            Name = "Yearly Plan",
            Description = "Best value - Save 17%",
            Price = 49.99m,
            Type = ProductType.Subscription,
            SubscriptionPeriod = Models.SubscriptionPeriod.Yearly,
            IconName = "verified",
            IconColor = "primary",
            Features = new() { "7-day free trial included", "Unlock all premium features" },
            IsBestValue = true,
            SavePercentage = 17,
            HasFreeTrial = true,
            FreeTrialDays = 7,
            AndroidProductId = "com.payment.pro.yearly",
            IosProductId = "com_payment_pro_yearly"
        },
        
        // One-Time Purchases
        new Product
        {
            Id = "remove_ads",
            Name = "Remove Ads",
            Description = "Distraction-free experience forever",
            Price = 2.99m,
            Type = ProductType.NonConsumable,
            IconName = "block",
            IconColor = "indigo",
            AndroidProductId = "com.payment.removeads",
            IosProductId = "com_payment_removeads"
        },
        new Product
        {
            Id = "coin_pack_500",
            Name = "Coin Pack (500)",
            Description = "Get a head start on your progress",
            Price = 0.99m,
            Type = ProductType.Consumable,
            IconName = "savings",
            IconColor = "amber",
            AndroidProductId = "com.payment.coins.500",
            IosProductId = "com_payment_coins_500"
        },
        new Product
        {
            Id = "super_boost",
            Name = "Super Boost",
            Description = "Triple XP for 24 hours",
            Price = 1.99m,
            Type = ProductType.Consumable,
            IconName = "rocket_launch",
            IconColor = "emerald",
            AndroidProductId = "com.payment.boost.super",
            IosProductId = "com_payment_boost_super"
        }
    };

    public Task<List<Product>> GetAllProductsAsync()
    {
        return Task.FromResult(_products);
    }

    public Task<List<Product>> GetSubscriptionProductsAsync()
    {
        return Task.FromResult(_products.Where(p => p.Type == ProductType.Subscription).ToList());
    }

    public Task<List<Product>> GetOneTimeProductsAsync()
    {
        return Task.FromResult(_products.Where(p => p.Type != ProductType.Subscription).ToList());
    }

    public Task<Product?> GetProductByIdAsync(string productId)
    {
        return Task.FromResult(_products.FirstOrDefault(p => p.Id == productId));
    }
}

