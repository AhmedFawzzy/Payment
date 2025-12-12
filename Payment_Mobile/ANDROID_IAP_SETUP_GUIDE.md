# 🤖 Android In-App Purchases Setup Guide

## 📋 Overview

This guide will help you set up **Google Play In-App Billing** for your Android app. You can test everything in a sandbox environment before going live!

---

## 🎯 Prerequisites

### What You Need:
1. ✅ **Google Play Console Account** ($25 one-time registration fee)
2. ✅ **Google Account for Testing** (can be a free Gmail account)
3. ✅ **Android Device or Emulator** (with Google Play Store)
4. ✅ **Your App Published** (at least to internal testing)

### What Your Client Needs to Provide:
**Nothing for basic testing!** You can test with your own Play Console account.

**For production backend validation (later):**
- 🔑 **Service Account JSON Key** (for server-side receipt validation)
- 📧 **Google Play Developer Account Email** (if they want to add you as a user)

---

## 📝 Step-by-Step Setup

### Phase 1: Google Play Console Setup (30 minutes)

#### Step 1: Create Google Play Console Account

1. Go to [Google Play Console](https://play.google.com/console)
2. Sign in with your Google account
3. Pay the $25 one-time registration fee
4. Complete the account details

#### Step 2: Create Your App

1. In Play Console, click **"Create app"**
2. Fill in app details:
   - **App name**: "Payment Test App" (or your app name)
   - **Default language**: English (United States)
   - **App or game**: App
   - **Free or paid**: Free
3. Click **"Create app"**

#### Step 3: Set Up In-App Products

##### For Subscriptions:

1. Go to **"Monetize" → "Subscriptions"**
2. Click **"Create subscription"**
3. Fill in details for **Monthly Plan**:
   ```
   Product ID: com.payment.pro.monthly
   Name: Monthly Plan
   Description: Premium access billed monthly
   ```
4. Set up **Base plan**:
   ```
   Base plan ID: monthly-base
   Billing period: 1 Month
   Price: $4.99 USD
   ```
5. Click **"Activate"**
6. Repeat for **Yearly Plan**:
   ```
   Product ID: com.payment.pro.yearly
   Base plan ID: yearly-base
   Billing period: 1 Year
   Price: $49.99 USD
   Free trial: 7 days
   ```

##### For One-Time Purchases:

1. Go to **"Monetize" → "In-app products"**
2. Click **"Create product"**
3. Create **Remove Ads**:
   ```
   Product ID: com.payment.removeads
   Name: Remove Ads
   Description: Remove all advertisements
   Price: $2.99 USD
   ```
4. Click **"Activate"**
5. Repeat for other products:
   - `com.payment.coins.500` - $0.99
   - `com.payment.boost.super` - $1.99

#### Step 4: Set Up License Testing

1. Go to **"Setup" → "License testing"**
2. Add test accounts (Gmail addresses):
   ```
   your.email@gmail.com
   another.test@gmail.com
   ```
3. Set **"License Test Response"** to:
   - ✅ **RESPOND_NORMALLY** (for testing purchases)

---

### Phase 2: App Configuration (15 minutes)

#### Step 1: Update AndroidManifest.xml

Add billing permission:

```xml
<uses-permission android:name="com.android.vending.BILLING" />
```

**File location**: `Payment_Mobile/Platforms/Android/AndroidManifest.xml`

Add this inside the `<manifest>` tag:

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <application android:allowBackup="true" 
                 android:icon="@mipmap/appicon" 
                 android:roundIcon="@mipmap/appicon_round" 
                 android:supportsRtl="true">
    </application>
    
    <!-- Add this line -->
    <uses-permission android:name="com.android.vending.BILLING" />
    
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.INTERNET" />
</manifest>
```

#### Step 2: Update Package Name

Your app's package name must match Play Console:

**File**: `Payment_Mobile/Payment_Mobile.csproj`

```xml
<ApplicationId>com.yourcompany.payment</ApplicationId>
```

Change to match your Play Console package name.

#### Step 3: Enable Real Purchases in Code

**File**: `Payment_Mobile/ViewModels/StoreViewModel.cs`

Comment out the demo code and uncomment the real purchase code:

```csharp
[RelayCommand]
private async Task PurchaseProductAsync(Product product)
{
    if (IsLoading || product == null) return;

    try
    {
        // Comment out demo code:
        // await Shell.Current.DisplayAlert("Purchase Demo", ...);

        // Uncomment real purchase code:
        IsLoading = true;
        var result = await _paymentService.PurchaseAsync(product);

        if (result.Success)
        {
            await Shell.Current.DisplayAlert(
                "Success!", 
                $"Purchase completed!\n\n{product.Name}\n{product.FormattedPrice}", 
                "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Purchase Failed", result.Message, "OK");
        }
        IsLoading = false;
    }
    catch (Exception ex)
    {
        await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        IsLoading = false;
    }
}
```

---

### Phase 3: Testing (30 minutes)

#### Step 1: Build Signed APK

1. In Visual Studio, right-click **Payment_Mobile (Android)**
2. Select **Archive for Publishing**
3. Create a new keystore (remember the password!)
4. Build the signed APK/AAB

#### Step 2: Upload to Internal Testing

1. In Play Console, go to **"Testing" → "Internal testing"**
2. Click **"Create new release"**
3. Upload your APK/AAB
4. Click **"Save"** and **"Review release"**
5. Click **"Start rollout to Internal testing"**

#### Step 3: Add Testers

1. In **Internal testing**, click **"Testers"**
2. Create email list with test accounts:
   ```
   your.email@gmail.com
   test.user@gmail.com
   ```
3. Save and copy the **"Opt-in URL"**

#### Step 4: Install and Test

1. On your Android device, open the **Opt-in URL**
2. Accept to become a tester
3. Download and install the app from Play Store
4. **Sign in with your test account** (the one you added to license testing)
5. Open the app and try purchasing!

---

## 🧪 Testing Environment Features

### What You Can Test (FREE):

✅ **All purchases are FREE for test accounts**
✅ **No real money charged**
✅ **Test subscriptions renew every 5 minutes** (not 1 month!)
✅ **Test free trials last 3 minutes** (not 7 days!)
✅ **Can test refunds**
✅ **Can test cancellations**

### Test Subscription Time Periods:

| Real Period | Test Period |
|------------|-------------|
| 7 days trial | 3 minutes |
| 1 month | 5 minutes |
| 3 months | 10 minutes |
| 6 months | 15 minutes |
| 1 year | 30 minutes |

This means you can test a full year subscription lifecycle in 30 minutes!

### Testing Checklist:

- [ ] Install app from Internal Testing
- [ ] Sign in with test account
- [ ] Purchase a subscription
- [ ] Verify purchase appears in your app
- [ ] Wait 5 minutes for renewal
- [ ] Check subscription renewed
- [ ] Cancel subscription
- [ ] Verify cancellation worked
- [ ] Test one-time purchases
- [ ] Test "Restore Purchases"

---

## 🔧 Troubleshooting

### Problem: "Item not found" error

**Solution:**
1. Make sure product IDs match exactly
2. Verify products are **activated** in Play Console
3. Wait 2-4 hours after creating products
4. Clear Play Store cache

### Problem: "App not found in Play Store"

**Solution:**
1. Ensure app is uploaded to Internal Testing
2. Check you're signed in with tester account
3. Try the opt-in URL again

### Problem: "This version of the app is not configured for billing"

**Solution:**
1. App must be signed with release keystore
2. App must be uploaded to Play Console (any track)
3. Package name must match Play Console
4. Wait 2-4 hours after first upload

### Problem: Purchases not working on emulator

**Solution:**
1. Emulator must have Google Play Store (not Google APIs)
2. Sign in to Play Store on emulator
3. Use a device instead if possible

---

## 📱 What Your Client Needs (Production)

### For Basic IAP (No Backend):
✅ **Nothing!** Plugin.InAppBilling handles everything client-side.

### For Server-Side Receipt Validation (Recommended):

1. **Service Account JSON Key**
   - Purpose: Validate receipts on your backend
   - How to get:
     ```
     1. Play Console → Setup → API access
     2. Create service account
     3. Grant access
     4. Download JSON key
     5. Send to your backend team
     ```

2. **Package Name**
   - Example: `com.payment.myapp`
   - Found in: Play Console → App details

3. **Product IDs List**
   - All subscription IDs
   - All in-app product IDs

---

## 🎯 Quick Start for Testing (TL;DR)

1. **Create Play Console account** ($25)
2. **Create app** in Play Console
3. **Add products** with exact IDs from your code
4. **Add your Gmail to license testers**
5. **Build signed APK**
6. **Upload to Internal Testing**
7. **Install on device using opt-in URL**
8. **Test purchases** (all FREE with test account!)

---

## 💡 Pro Tips

### For Development:
- ✅ Use Internal Testing (fastest updates)
- ✅ Add multiple test accounts
- ✅ Test on real devices (emulators can be tricky)
- ✅ Keep Play Console open to monitor purchases

### For Production:
- ✅ Always validate receipts on server
- ✅ Store purchase tokens in database
- ✅ Implement proper error handling
- ✅ Test refund scenarios
- ✅ Monitor Play Console for issues

---

## 📚 Additional Resources

### Official Documentation:
- [Google Play Billing Overview](https://developer.android.com/google/play/billing/integrate)
- [Test In-App Purchases](https://developer.android.com/google/play/billing/test)
- [Plugin.InAppBilling Docs](https://github.com/jamesmontemagno/InAppBillingPlugin)

### Video Tutorials:
- [Google Play Billing Setup](https://www.youtube.com/results?search_query=google+play+billing+setup)
- [MAUI In-App Purchases](https://www.youtube.com/results?search_query=maui+in+app+purchases)

---

## ✅ Summary

**To enable real Android IAP:**

1. ✅ Create Play Console account
2. ✅ Set up products in Play Console
3. ✅ Add test accounts
4. ✅ Upload signed APK to Internal Testing
5. ✅ Test with test accounts (FREE!)
6. ✅ All purchases are simulated (no charges)

**Your client needs to provide:**
- **Nothing for testing!**
- **Service Account JSON** (only for production backend validation)

---

**You're ready to test real purchases! 🎉**

Need help with any step? The community at r/androiddev and Stack Overflow are very helpful!

