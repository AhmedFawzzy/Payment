# 📦 Product IDs Configuration

## Overview

These are the product IDs your app uses. They must match **exactly** in:
1. ✅ Your code (ProductService.cs)
2. ✅ Google Play Console
3. ✅ App Store Connect (iOS)

---

## 🔐 Current Product IDs

### Subscriptions

| Name | Product ID | Price | Period | Free Trial |
|------|-----------|-------|--------|------------|
| Monthly Plan | `com.payment.pro.monthly` | $4.99 | Monthly | No |
| Yearly Plan | `com.payment.pro.yearly` | $49.99 | Yearly | 7 days |

### One-Time Purchases

| Name | Product ID | Price | Type |
|------|-----------|-------|------|
| Remove Ads | `com.payment.removeads` | $2.99 | Non-consumable |
| 500 Coins | `com.payment.coins.500` | $0.99 | Consumable |
| Super Boost | `com.payment.boost.super` | $1.99 | Consumable |

---

## 📝 How to Set Up in Google Play Console

### For Subscriptions:

1. Go to **Monetize → Subscriptions**
2. Click **Create subscription**
3. Enter:
   ```
   Product ID: com.payment.pro.monthly
   Name: Monthly Plan
   Description: Premium features with monthly billing
   ```
4. Click **Add base plan**:
   ```
   Base plan ID: monthly-base
   Billing period: 1 Month
   Price: $4.99 USD
   ```
5. Click **Activate**

Repeat for `com.payment.pro.yearly` with:
- Base plan: `yearly-base`
- Billing period: 1 Year
- Price: $49.99 USD
- **Free trial: 7 days** (optional)

### For One-Time Purchases:

1. Go to **Monetize → In-app products**
2. Click **Create product**
3. Enter:
   ```
   Product ID: com.payment.removeads
   Name: Remove Ads
   Description: Permanently remove all advertisements
   Price: $2.99 USD
   ```
4. Click **Activate**

Repeat for other products.

---

## 🔧 Naming Convention

Current format: `com.payment.[category].[name]`

Examples:
- `com.payment.pro.monthly` - Pro subscription, monthly
- `com.payment.removeads` - Remove ads feature
- `com.payment.coins.500` - 500 coins pack
- `com.payment.boost.super` - Super boost power-up

### To Customize:

Replace `com.payment` with your package name:

```
com.yourcompany.yourapp.pro.monthly
com.yourcompany.yourapp.removeads
```

**Important**: Must be lowercase, alphanumeric with dots and underscores only.

---

## 🛠️ Where to Update Product IDs

### In Your Code:

**File**: `Payment_Mobile/Services/ProductService.cs`

```csharp
public Task<List<Product>> GetAllProductsAsync()
{
    var products = new List<Product>
    {
        // SUBSCRIPTIONS
        new Product 
        { 
            Id = "com.payment.pro.monthly",  // ⬅️ Change this
            Name = "Monthly Plan",
            // ...
        },
        new Product 
        { 
            Id = "com.payment.pro.yearly",   // ⬅️ Change this
            Name = "Yearly Plan",
            // ...
        },
        
        // ONE-TIME PURCHASES
        new Product 
        { 
            Id = "com.payment.removeads",    // ⬅️ Change this
            Name = "Remove Ads",
            // ...
        },
        // ...
    };
    
    return Task.FromResult(products);
}
```

### In Backend (for validation):

**File**: `Payment_Backend/Controllers/ProductsController.cs`

Product IDs must match when validating purchases.

---

## ✅ Verification Checklist

Before testing:

- [ ] All product IDs are lowercase
- [ ] No spaces in product IDs
- [ ] IDs match in code and Play Console exactly
- [ ] Products are **Activated** in Play Console
- [ ] Subscription base plans are configured
- [ ] Prices are set correctly
- [ ] Free trial configured (if applicable)

---

## 🧪 Testing Product IDs

You can test product IDs are correct by:

1. Run app with `USE_DEMO_MODE = false`
2. If you see "Item not found", IDs don't match
3. If you see price and can purchase, IDs are correct!

---

## 💡 Pro Tips

### For Development:
- Use `.test` suffix for test products:
  ```
  com.payment.pro.monthly.test
  ```
- Easier to distinguish from production

### For Production:
- Use simple, descriptive names
- Group by category (e.g., `pro.*`, `coins.*`)
- Document all IDs in this file
- Never reuse deleted product IDs

---

## 📱 Platform-Specific Notes

### Android (Google Play):
- Product IDs are case-sensitive
- Must be unique across your developer account
- Cannot be deleted (can only be deactivated)
- Format: lowercase letters, numbers, dots, underscores

### iOS (App Store):
- Product IDs are case-insensitive
- Must be unique across your app
- Can be deleted if never used
- Format: reverse domain notation recommended

**Tip**: Use the same IDs for both platforms for easier maintenance!

---

## 🔄 How to Change Product IDs

**WARNING**: Changing product IDs requires:
1. Creating new products in store consoles
2. Updating code
3. Handling migration for existing users
4. Can't transfer purchase history

**Better approach**: Leave existing IDs, add new ones with different names.

---

**Questions?** See `ANDROID_IAP_SETUP_GUIDE.md` for setup help!

