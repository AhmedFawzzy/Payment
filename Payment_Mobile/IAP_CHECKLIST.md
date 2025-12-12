# ✅ In-App Purchase Setup Checklist

Print this out or keep it handy while setting up!

---

## 📋 Phase 1: Google Play Console Setup

### Account Creation
- [ ] Go to https://play.google.com/console
- [ ] Sign in with Google account
- [ ] Pay $25 registration fee
- [ ] Complete developer profile
- [ ] Accept terms and conditions

**Time:** 15 minutes | **Cost:** $25 one-time

---

### App Creation
- [ ] Click "Create app"
- [ ] Enter app name
- [ ] Select "App" (not game)
- [ ] Select "Free" (not paid)
- [ ] Complete app details
- [ ] Save app

**Time:** 10 minutes | **Cost:** FREE

---

### Product Creation: Subscriptions

**Monthly Plan:**
- [ ] Go to "Monetize" → "Subscriptions"
- [ ] Click "Create subscription"
- [ ] Product ID: `com.payment.pro.monthly`
- [ ] Name: "Monthly Plan"
- [ ] Description: "Premium access billed monthly"
- [ ] Add base plan: "monthly-base"
- [ ] Billing period: 1 Month
- [ ] Price: $4.99 USD
- [ ] Click "Activate"

**Yearly Plan:**
- [ ] Click "Create subscription"
- [ ] Product ID: `com.payment.pro.yearly`
- [ ] Name: "Yearly Plan"
- [ ] Description: "Premium access billed yearly"
- [ ] Add base plan: "yearly-base"
- [ ] Billing period: 1 Year
- [ ] Price: $49.99 USD
- [ ] Free trial: 7 days
- [ ] Click "Activate"

**Time:** 15 minutes | **Cost:** FREE

---

### Product Creation: One-Time Purchases

**Remove Ads:**
- [ ] Go to "Monetize" → "In-app products"
- [ ] Click "Create product"
- [ ] Product ID: `com.payment.removeads`
- [ ] Name: "Remove Ads"
- [ ] Description: "Remove all advertisements"
- [ ] Price: $2.99 USD
- [ ] Click "Activate"

**500 Coins:**
- [ ] Click "Create product"
- [ ] Product ID: `com.payment.coins.500`
- [ ] Name: "500 Coins"
- [ ] Description: "Get 500 coins"
- [ ] Price: $0.99 USD
- [ ] Click "Activate"

**Super Boost:**
- [ ] Click "Create product"
- [ ] Product ID: `com.payment.boost.super`
- [ ] Name: "Super Boost"
- [ ] Description: "Activate super boost"
- [ ] Price: $1.99 USD
- [ ] Click "Activate"

**Time:** 10 minutes | **Cost:** FREE

---

### Testing Setup

- [ ] Go to "Setup" → "License testing"
- [ ] Add test email #1: ________________@gmail.com
- [ ] Add test email #2: ________________@gmail.com
- [ ] Add test email #3: ________________@gmail.com
- [ ] Set "License Test Response" to "RESPOND_NORMALLY"
- [ ] Save changes

**Time:** 5 minutes | **Cost:** FREE

---

## 📋 Phase 2: App Configuration

### Update AndroidManifest.xml

- [ ] Open `Payment_Mobile/Platforms/Android/AndroidManifest.xml`
- [ ] Add billing permission inside `<manifest>` tag:
  ```xml
  <uses-permission android:name="com.android.vending.BILLING" />
  ```
- [ ] Save file

**Time:** 2 minutes

---

### Verify Product IDs

- [ ] Open `Payment_Mobile/Services/ProductService.cs`
- [ ] Verify Monthly Plan ID: `com.payment.pro.monthly`
- [ ] Verify Yearly Plan ID: `com.payment.pro.yearly`
- [ ] Verify Remove Ads ID: `com.payment.removeads`
- [ ] Verify 500 Coins ID: `com.payment.coins.500`
- [ ] Verify Super Boost ID: `com.payment.boost.super`

**Time:** 2 minutes

---

### Update Package Name (if needed)

- [ ] Open `Payment_Mobile/Payment_Mobile.csproj`
- [ ] Find `<ApplicationId>` tag
- [ ] Set to match Play Console app package name
- [ ] Save file

**Time:** 2 minutes

---

## 📋 Phase 3: Build & Upload

### Create Keystore (First Time Only)

- [ ] Right-click Android project in Solution Explorer
- [ ] Select "Archive..."
- [ ] Wait for archive to complete
- [ ] Click "Distribute"
- [ ] Select "Ad Hoc"
- [ ] Click "Create new signing identity"
- [ ] Fill in keystore details:
  - [ ] Alias: ________________
  - [ ] Password: ________________ (SAVE THIS!)
  - [ ] Validity: 25 years
- [ ] Save keystore location: ________________

**Time:** 10 minutes | **IMPORTANT:** Save password!

---

### Build Signed APK/AAB

- [ ] Right-click Android project
- [ ] Select "Archive..."
- [ ] Wait for build to complete
- [ ] Click "Distribute"
- [ ] Select "Google Play"
- [ ] Select existing signing identity
- [ ] Save APK/AAB to known location
- [ ] Note file location: ________________

**Time:** 15 minutes

---

### Upload to Internal Testing

- [ ] Go to Play Console
- [ ] Open your app
- [ ] Go to "Testing" → "Internal testing"
- [ ] Click "Create new release"
- [ ] Upload APK/AAB file
- [ ] Enter release notes (optional)
- [ ] Click "Save"
- [ ] Click "Review release"
- [ ] Check for any errors
- [ ] Click "Start rollout to Internal testing"
- [ ] Wait for processing (2-4 hours)

**Time:** 10 minutes + 2-4 hours wait

---

### Add Testers

- [ ] In "Internal testing", click "Testers" tab
- [ ] Create new email list
- [ ] Add test emails
- [ ] Save list
- [ ] Copy "Opt-in URL"
- [ ] Opt-in URL: ________________________________

**Time:** 5 minutes

---

## 📋 Phase 4: Testing (Demo Mode)

### Install App

- [ ] On Android device, open opt-in URL
- [ ] Accept to become tester
- [ ] Click "Download on Play Store"
- [ ] Install app
- [ ] Open app

**Time:** 5 minutes

---

### Test Demo Mode

- [ ] App opens successfully
- [ ] Products load and display
- [ ] Can switch between Subscriptions/One-Time tabs
- [ ] Can click on a product
- [ ] Demo dialog appears with product info
- [ ] Demo dialog mentions demo mode
- [ ] All products clickable
- [ ] No crashes

**Time:** 10 minutes

---

## 📋 Phase 5: Enable Real Purchases

### Update Code

- [ ] Open `Payment_Mobile/ViewModels/StoreViewModel.cs`
- [ ] Find line: `private const bool USE_DEMO_MODE = true;`
- [ ] Change to: `private const bool USE_DEMO_MODE = false;`
- [ ] Save file
- [ ] Build solution (check for errors)

**Time:** 2 minutes

---

### Build & Upload Again

- [ ] Archive for publishing (see Phase 3)
- [ ] Upload new version to Internal Testing
- [ ] Wait for processing (2-4 hours)
- [ ] Update app on device

**Time:** 15 minutes + 2-4 hours wait

---

## 📋 Phase 6: Testing (Real Purchases)

### Pre-Test Checklist

- [ ] App installed from Internal Testing
- [ ] Signed in with test account (from license testing)
- [ ] Internet connection active
- [ ] Google Play Store signed in
- [ ] At least 2-4 hours since product creation

**Time:** 5 minutes

---

### Test Subscriptions

**Monthly Plan:**
- [ ] Click on Monthly Plan
- [ ] Google Play dialog appears
- [ ] Shows $0.00 or "Test subscription"
- [ ] Click "Subscribe"
- [ ] Purchase completes
- [ ] Success message appears
- [ ] No errors

**Yearly Plan (with trial):**
- [ ] Click on Yearly Plan
- [ ] Google Play dialog shows free trial
- [ ] Click "Subscribe"
- [ ] Purchase completes
- [ ] Success message appears
- [ ] No errors

**Time:** 10 minutes

---

### Test One-Time Purchases

**Remove Ads:**
- [ ] Click on Remove Ads
- [ ] Google Play dialog appears
- [ ] Shows $0.00 or "Test purchase"
- [ ] Click "Buy"
- [ ] Purchase completes
- [ ] Success message appears

**500 Coins:**
- [ ] Click on 500 Coins
- [ ] Purchase dialog appears
- [ ] Complete purchase
- [ ] Success message

**Super Boost:**
- [ ] Click on Super Boost
- [ ] Purchase dialog appears
- [ ] Complete purchase
- [ ] Success message

**Time:** 10 minutes

---

### Test Advanced Features

**Subscription Renewal:**
- [ ] Wait 5 minutes after subscription purchase
- [ ] Subscription should auto-renew
- [ ] Check Play Console for renewal confirmation

**Restore Purchases:**
- [ ] Uninstall app
- [ ] Reinstall app
- [ ] Click "Restore Purchases"
- [ ] Previous purchases restored
- [ ] Success message

**Cancellation:**
- [ ] Go to Play Store → Subscriptions
- [ ] Find your test subscription
- [ ] Cancel subscription
- [ ] Verify cancellation works

**Time:** 20 minutes

---

## 📋 Phase 7: Production Preparation (Optional)

### Backend Receipt Validation

- [ ] Get Service Account JSON from client
- [ ] Implement server-side validation
- [ ] Test validation flow
- [ ] Handle validation errors

**Time:** Varies | **Required:** Only for production

---

### Final Checks

- [ ] All purchases tested successfully
- [ ] Error handling works
- [ ] UI handles all states
- [ ] No crashes
- [ ] Good user experience
- [ ] Analytics implemented (optional)
- [ ] Support system ready

**Time:** 30 minutes

---

## ✅ Completion Summary

### Phase 1: Google Play Console
- [ ] Account created
- [ ] App created
- [ ] 5 products created and activated
- [ ] Test accounts added

### Phase 2: App Configuration
- [ ] AndroidManifest updated
- [ ] Product IDs verified
- [ ] Package name correct

### Phase 3: Build & Upload
- [ ] Keystore created (password saved!)
- [ ] APK/AAB built
- [ ] Uploaded to Internal Testing
- [ ] Testers added

### Phase 4: Demo Testing
- [ ] App installed
- [ ] Demo mode tested
- [ ] UI verified

### Phase 5: Real Purchase Setup
- [ ] Code updated (USE_DEMO_MODE = false)
- [ ] New version uploaded
- [ ] App updated

### Phase 6: Real Purchase Testing
- [ ] Subscriptions tested
- [ ] One-time purchases tested
- [ ] Advanced features tested
- [ ] All working!

### Phase 7: Production (Optional)
- [ ] Backend validation (if needed)
- [ ] Final checks complete

---

## 🎊 Success Criteria

**You're ready for production when:**

✅ All checkboxes above are checked
✅ All test purchases work perfectly
✅ No crashes or errors
✅ Subscriptions renew correctly
✅ Restore purchases works
✅ Good error handling
✅ Clear user feedback

---

## 📝 Important Information to Save

### Keystore Details (SAVE THIS!)
```
Keystore Location: ________________________________
Alias: ________________________________
Password: ________________________________
Created: ___/___/______
```

### Play Console
```
Developer Account Email: ________________________________
App Package Name: ________________________________
First Upload Date: ___/___/______
```

### Test Accounts
```
Test Email 1: ________________________________
Test Email 2: ________________________________
Test Email 3: ________________________________
```

### Opt-In URL
```
Internal Testing URL: ________________________________
```

---

## 🆘 Troubleshooting Checklist

### "Item not found" error
- [ ] Waited 2-4 hours after creating products?
- [ ] Product IDs match exactly?
- [ ] Products activated in Play Console?
- [ ] Cleared Play Store cache?

### "App not configured for billing"
- [ ] Uploaded signed APK first?
- [ ] Waited 2-4 hours after upload?
- [ ] Package name matches?
- [ ] Billing permission in AndroidManifest?

### Purchase not working
- [ ] Using test account from license testing?
- [ ] Signed in to Play Store?
- [ ] Internet connection active?
- [ ] App from Internal Testing (not sideload)?

### Emulator issues
- [ ] Using Play Store system image?
- [ ] Play Store signed in?
- [ ] Try real device instead?

---

**Print this checklist and check off items as you go!**

**Estimated Total Time:** 1.5 hours active + 4-8 hours waiting

**Total Cost:** $25 (one-time)

**Result:** Production-ready In-App Purchase system! 🎉

