# 🚀 Quick Start: Enable Real Android Purchases

## Current Status: DEMO MODE ✨

Your app is currently in **demo mode** - clicks show a dialog instead of real purchases.

---

## 🎯 How to Enable Real Purchases

### Step 1: Set Up Google Play Console (One-time)

```
1. Create account at play.google.com/console ($25)
2. Create app
3. Add these products in "Monetize" section:

   SUBSCRIPTIONS:
   - com.payment.pro.monthly ($4.99/month)
   - com.payment.pro.yearly ($49.99/year, 7-day trial)
   
   ONE-TIME PURCHASES:
   - com.payment.removeads ($2.99)
   - com.payment.coins.500 ($0.99)
   - com.payment.boost.super ($1.99)

4. Add test email in "License testing"
5. Upload signed APK to "Internal Testing"
```

### Step 2: Update Your Code (2 minutes)

**File**: `Payment_Mobile/ViewModels/StoreViewModel.cs`

**Change line 13:**

```csharp
// FROM:
private const bool USE_DEMO_MODE = true;

// TO:
private const bool USE_DEMO_MODE = false;
```

### Step 3: Update Product IDs (if needed)

**File**: `Payment_Mobile/Services/ProductService.cs`

Make sure product IDs match what you created in Play Console:

```csharp
new Product 
{ 
    Id = "com.payment.pro.monthly",  // ✅ Must match Play Console exactly
    Name = "Monthly Plan",
    // ...
}
```

### Step 4: Test!

1. Build signed APK
2. Upload to Internal Testing
3. Install on device via opt-in URL
4. Sign in with test account
5. Try purchasing - **IT'S FREE for test accounts!**

---

## 🧪 Testing Environment

**All purchases are FREE for test accounts!**

| Feature | Demo Mode | Test Mode | Production |
|---------|-----------|-----------|------------|
| Cost | Free | Free ✅ | Real money |
| Setup needed | None | Play Console | Play Console |
| Test time | Instant | 5 min/sub | 1 month/sub |
| Requires APK upload | No | Yes | Yes |

**Test subscriptions renew every 5 minutes** (not 1 month), so you can test everything quickly!

---

## 📋 What Your Client Needs

### For Testing:
**Nothing!** You can test with your own Play Console account.

### For Production (later):
- Service Account JSON key (for backend receipt validation)
- Package name (e.g., `com.payment.myapp`)
- List of product IDs

---

## 🐛 Common Issues

### "Item not found"
- Wait 2-4 hours after creating products
- Check product IDs match exactly
- Verify products are "Activated"

### "App not configured for billing"
- Must upload signed APK to Play Console first
- Package name must match
- Wait 2-4 hours after first upload

### Emulator not working
- Use device with Play Store
- Or use emulator with Play Store (not Google APIs)

---

## 📚 Full Documentation

See **`ANDROID_IAP_SETUP_GUIDE.md`** for:
- Complete step-by-step setup
- Detailed troubleshooting
- Testing best practices
- Production deployment guide

---

## ✅ Quick Checklist

Before switching to real purchase mode:

- [ ] Play Console account created
- [ ] App created in Play Console
- [ ] All products created and activated
- [ ] Test account added to license testing
- [ ] Signed APK uploaded to Internal Testing
- [ ] Product IDs match in code
- [ ] `USE_DEMO_MODE = false` set
- [ ] App installed on device via opt-in URL
- [ ] Ready to test!

---

**Need help?** Check `ANDROID_IAP_SETUP_GUIDE.md` for detailed instructions!

