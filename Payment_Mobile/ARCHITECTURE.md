# 🏗️ Payment System Architecture

## System Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                         MOBILE APP (MAUI)                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ┌──────────────┐      ┌──────────────┐      ┌──────────────┐  │
│  │   StorePage  │      │ Subscription │      │  Confirmation│  │
│  │              │      │ DetailsPage  │      │     Page     │  │
│  │  (View)      │      │              │      │              │  │
│  │              │      │  (View)      │      │  (View)      │  │
│  └──────┬───────┘      └──────┬───────┘      └──────┬───────┘  │
│         │                     │                     │            │
│         └─────────────────────┼─────────────────────┘            │
│                               │                                  │
│  ┌────────────────────────────▼──────────────────────────────┐  │
│  │                      ViewModels Layer                      │  │
│  ├────────────────────────────────────────────────────────────┤  │
│  │  • StoreViewModel           (Product listing & purchase)   │  │
│  │  • SubscriptionDetailsViewModel  (Subscription details)    │  │
│  │  • PaymentConfirmationViewModel  (Purchase confirmation)   │  │
│  └────────────────────────────┬──────────────────────────────┘  │
│                               │                                  │
│  ┌────────────────────────────▼──────────────────────────────┐  │
│  │                      Services Layer                        │  │
│  ├────────────────────────────────────────────────────────────┤  │
│  │                                                             │  │
│  │  ┌──────────────────┐      ┌──────────────────┐           │  │
│  │  │  IPaymentService │      │   IApiService    │           │  │
│  │  │                  │      │                  │           │  │
│  │  │  • PurchaseAsync │      │  • GetProducts   │           │  │
│  │  │  • RestorePurchases│    │  • ValidatePurchase│         │  │
│  │  │  • GetPurchases  │      │  • GetHistory    │           │  │
│  │  └────────┬─────────┘      └────────┬─────────┘           │  │
│  │           │                         │                      │  │
│  │           │                         │                      │  │
│  │  ┌────────▼─────────┐      ┌────────▼─────────┐           │  │
│  │  │ PaymentService   │      │   ApiService     │           │  │
│  │  │  (Implementation)│      │  (Implementation)│           │  │
│  │  │                  │      │                  │           │  │
│  │  │ Uses:            │      │ Uses:            │           │  │
│  │  │ Plugin.InAppBilling│    │ HttpClient       │           │  │
│  │  └────────┬─────────┘      └────────┬─────────┘           │  │
│  │           │                         │                      │  │
│  └───────────┼─────────────────────────┼──────────────────────┘  │
│              │                         │                          │
│  ┌───────────▼─────────────────────────▼──────────────────────┐  │
│  │                      Models Layer                           │  │
│  ├─────────────────────────────────────────────────────────────┤  │
│  │  • Product        (Product information)                     │  │
│  │  • Purchase       (Purchase records)                        │  │
│  │  • PurchaseResult (Transaction results)                     │  │
│  │  • Platform       (iOS/Android/OneTime/Subscription)        │  │
│  └─────────────────────────────────────────────────────────────┘  │
│                                                                   │
└───────────────────────────┬───────────────────────────────────────┘
                            │
        ┌───────────────────┼───────────────────┐
        │                   │                   │
        ▼                   ▼                   ▼
┌───────────────┐   ┌───────────────┐   ┌───────────────┐
│  Google Play  │   │  App Store    │   │    Backend    │
│   Billing     │   │   StoreKit    │   │      API      │
├───────────────┤   ├───────────────┤   ├───────────────┤
│ • Products    │   │ • Products    │   │ • Validation  │
│ • Purchases   │   │ • Purchases   │   │ • History     │
│ • Receipts    │   │ • Receipts    │   │ • Analytics   │
└───────────────┘   └───────────────┘   └───────────────┘
```

---

## Data Flow

### 1️⃣ Loading Products

```
User Opens App
      │
      ▼
┌─────────────────┐
│  StorePage.xaml │
│  OnAppearing()  │
└────────┬────────┘
         │
         ▼
┌──────────────────────┐
│  StoreViewModel      │
│  LoadProductsAsync() │
└────────┬─────────────┘
         │
         ├─────────────────────────┐
         │                         │
         ▼                         ▼
┌──────────────────┐     ┌──────────────────┐
│  ApiService      │     │ PaymentService   │
│  GetSubscriptions│     │ (Gets prices     │
│  GetOneTime()    │     │  from stores)    │
└────────┬─────────┘     └────────┬─────────┘
         │                         │
         ▼                         ▼
┌──────────────────┐     ┌──────────────────┐
│  Backend API     │     │ Google Play /    │
│  /api/Products   │     │ App Store        │
└────────┬─────────┘     └────────┬─────────┘
         │                         │
         └─────────────┬───────────┘
                       │
                       ▼
              ┌────────────────┐
              │  Products      │
              │  Displayed     │
              └────────────────┘
```

---

### 2️⃣ Purchase Flow (Demo Mode)

```
User Clicks Product
      │
      ▼
┌───────────────────────┐
│  StoreViewModel       │
│  PurchaseProductAsync │
│  if (USE_DEMO_MODE)   │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  Display Demo Alert   │
│  • Product name       │
│  • Price              │
│  • Type               │
│  • Instructions       │
└───────────────────────┘
```

---

### 3️⃣ Purchase Flow (Real Mode)

```
User Clicks Product
      │
      ▼
┌───────────────────────┐
│  StoreViewModel       │
│  PurchaseProductAsync │
│  if (!USE_DEMO_MODE)  │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  PaymentService       │
│  PurchaseAsync()      │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  Plugin.InAppBilling  │
│  PurchaseAsync()      │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  Google Play Store    │
│  Shows Purchase Dialog│
└───────────┬───────────┘
            │
     ┌──────┴──────┐
     │             │
   Success      Cancelled
     │             │
     ▼             ▼
┌─────────┐   ┌─────────┐
│ Receipt │   │  Error  │
└────┬────┘   └────┬────┘
     │             │
     └──────┬──────┘
            │
            ▼
┌───────────────────────┐
│  Validate Receipt     │
│  (Client-side)        │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  Optional:            │
│  Backend Validation   │
│  POST /Purchases/     │
│       validate        │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│  Show Success Dialog  │
│  Update UI            │
└───────────────────────┘
```

---

## Component Responsibilities

### 📱 Views (XAML Pages)

**StorePage.xaml**
- Displays product grid
- Subscription/One-Time tabs
- Product cards with info
- Loading indicators

**SubscriptionDetailsPage.xaml**
- Shows subscription details
- Features list
- Pricing information
- Subscribe button

**PaymentConfirmationPage.xaml**
- Success animation
- Purchase summary
- Download/Access button
- Receipt information

---

### 🎯 ViewModels

**StoreViewModel**
```csharp
Responsibilities:
├─ Load products from API
├─ Handle purchase initiation
├─ Manage loading states
├─ Toggle between product types
└─ Demo mode vs Real mode
```

**SubscriptionDetailsViewModel**
```csharp
Responsibilities:
├─ Display subscription details
├─ Handle subscription purchase
├─ Manage free trial info
└─ Navigate to confirmation
```

**PaymentConfirmationViewModel**
```csharp
Responsibilities:
├─ Show purchase confirmation
├─ Display receipt details
├─ Handle navigation back
└─ Optional: Track analytics
```

---

### 🔧 Services

**IPaymentService / PaymentService**
```csharp
Responsibilities:
├─ Platform-specific billing integration
├─ Purchase execution
├─ Receipt retrieval
├─ Purchase restoration
├─ Connection management
└─ Error handling

Uses:
└─ Plugin.InAppBilling
   ├─ CrossInAppBilling.Current
   ├─ ConnectAsync()
   ├─ PurchaseAsync()
   ├─ GetPurchasesAsync()
   └─ DisconnectAsync()
```

**IApiService / ApiService**
```csharp
Responsibilities:
├─ Backend communication
├─ Product catalog retrieval
├─ Purchase validation
├─ Purchase history
└─ Error handling

Uses:
└─ HttpClient
   ├─ GET /api/Products
   ├─ GET /api/Products/subscriptions
   ├─ POST /api/Purchases/validate
   └─ GET /api/Purchases/history/{userId}
```

---

### 📦 Models

**Product**
```csharp
Properties:
├─ Id (string) - Store product ID
├─ Name (string) - Display name
├─ Description (string)
├─ Price (decimal) - Numeric price
├─ FormattedPrice (string) - "$4.99"
├─ Type (ProductType) - Subscription/OneTime
├─ Platform (Platform) - Android/iOS
├─ SubscriptionPeriod (string) - "Monthly"
├─ HasFreeTrial (bool)
├─ FreeTrialDays (int)
└─ Features (List<string>)
```

**Purchase**
```csharp
Properties:
├─ Id (string)
├─ ProductId (string)
├─ UserId (string)
├─ TransactionId (string)
├─ PurchaseToken (string)
├─ PurchaseDate (DateTime)
├─ ExpirationDate (DateTime?)
├─ IsAcknowledged (bool)
├─ Platform (Platform)
└─ Receipt (string)
```

**PurchaseResult**
```csharp
Properties:
├─ Success (bool)
├─ Message (string)
└─ Purchase (Purchase?)
```

---

## Configuration

### App Startup (MauiProgram.cs)

```csharp
builder.Services Configuration:

├─ ViewModels (Transient)
│  ├─ StoreViewModel
│  ├─ SubscriptionDetailsViewModel
│  └─ PaymentConfirmationViewModel
│
├─ Services (Singleton)
│  ├─ IPaymentService → PaymentService
│  ├─ IProductService → ProductService
│  └─ IApiService → ApiService
│
├─ HttpClient
│  └─ BaseAddress: https://localhost:7014/api
│
└─ Views (Transient)
   ├─ StorePage
   ├─ SubscriptionDetailsPage
   └─ PaymentConfirmationPage
```

---

## Testing Architecture

### Demo Mode
```
┌─────────────────────────────────────┐
│  Demo Mode (USE_DEMO_MODE = true)  │
├─────────────────────────────────────┤
│                                     │
│  User Click                         │
│       │                             │
│       ▼                             │
│  ┌─────────────────┐                │
│  │ Show Alert      │                │
│  │ • Product info  │                │
│  │ • Price         │                │
│  │ • Instructions  │                │
│  └─────────────────┘                │
│                                     │
│  ✅ No store connection needed      │
│  ✅ No purchase flow               │
│  ✅ Instant testing                 │
│                                     │
└─────────────────────────────────────┘
```

### Real Purchase Mode
```
┌─────────────────────────────────────┐
│  Real Mode (USE_DEMO_MODE = false) │
├─────────────────────────────────────┤
│                                     │
│  User Click                         │
│       │                             │
│       ▼                             │
│  ┌─────────────────┐                │
│  │ PaymentService  │                │
│  └────────┬────────┘                │
│           │                         │
│           ▼                         │
│  ┌─────────────────┐                │
│  │ Google Play     │                │
│  │ Purchase Dialog │                │
│  └────────┬────────┘                │
│           │                         │
│           ▼                         │
│  ┌─────────────────┐                │
│  │ Real Purchase   │                │
│  │ (FREE for test) │                │
│  └─────────────────┘                │
│                                     │
│  ✅ Full store integration          │
│  ✅ Real purchase flow              │
│  ✅ Production-ready testing        │
│                                     │
└─────────────────────────────────────┘
```

---

## Platform Abstraction

### Cross-Platform Support

```
┌──────────────────────────────────────────────────┐
│              IPaymentService (Interface)          │
│  • PurchaseAsync()                               │
│  • RestorePurchasesAsync()                       │
│  • GetPurchaseHistoryAsync()                     │
└─────────────────────┬────────────────────────────┘
                      │
         ┌────────────┼────────────┐
         │                         │
         ▼                         ▼
┌────────────────┐         ┌────────────────┐
│ Android Impl   │         │   iOS Impl     │
├────────────────┤         ├────────────────┤
│ • Google Play  │         │ • StoreKit     │
│   Billing      │         │ • App Store    │
│ • Product IDs  │         │ • Product IDs  │
│   (Android)    │         │   (iOS)        │
└────────────────┘         └────────────────┘
```

**Current Implementation:**
- ✅ Android (via Plugin.InAppBilling)
- 🔄 iOS (via Plugin.InAppBilling - same interface)
- 🔄 macOS (via Plugin.InAppBilling - same interface)

---

## Security Layers

### Client-Side Validation
```
Purchase
    ↓
Plugin.InAppBilling
    ↓
Receipt Verification
    ↓
Basic Validation
```

### Server-Side Validation (Optional)
```
Purchase
    ↓
Get Receipt Token
    ↓
POST to Backend
    ↓
Backend Verifies with Google/Apple
    ↓
Database Storage
    ↓
Confirmation to Client
```

---

## Error Handling Strategy

```
┌─────────────────────────────────────┐
│         Error Occurs                │
└─────────────┬───────────────────────┘
              │
              ▼
┌─────────────────────────────────────┐
│  Caught in ViewModel                │
│  try { ... } catch (Exception ex)   │
└─────────────┬───────────────────────┘
              │
              ▼
┌─────────────────────────────────────┐
│  Log to Debug Output                │
│  System.Diagnostics.Debug.WriteLine │
└─────────────┬───────────────────────┘
              │
              ▼
┌─────────────────────────────────────┐
│  Show User-Friendly Alert           │
│  await Shell.Current.DisplayAlert   │
└─────────────┬───────────────────────┘
              │
              ▼
┌─────────────────────────────────────┐
│  Reset Loading State                │
│  IsLoading = false                  │
└─────────────────────────────────────┘
```

---

## Backend Integration

```
┌──────────────────────────────────────────────┐
│           ASP.NET Core Backend               │
├──────────────────────────────────────────────┤
│                                              │
│  Controllers:                                │
│  ├─ ProductsController                       │
│  │  ├─ GET /api/Products                     │
│  │  ├─ GET /api/Products/subscriptions       │
│  │  ├─ GET /api/Products/one-time            │
│  │  └─ GET /api/Products/{id}                │
│  │                                           │
│  ├─ PurchasesController                      │
│  │  ├─ POST /api/Purchases/validate          │
│  │  └─ GET /api/Purchases/history/{userId}   │
│  │                                           │
│  └─ SubscriptionsController                  │
│     ├─ GET /api/Subscriptions                │
│     └─ POST /api/Subscriptions/cancel        │
│                                              │
│  Services:                                   │
│  ├─ IProductService                          │
│  ├─ IPurchaseService                         │
│  └─ ISubscriptionService                     │
│                                              │
└──────────────────────────────────────────────┘
```

---

## Deployment Architecture

### Development
```
Developer Machine
    ↓
Debug Build
    ↓
Local Device/Emulator
    ↓
Demo Mode Testing
```

### Testing
```
Developer Machine
    ↓
Release Build (Signed)
    ↓
Play Console (Internal Testing)
    ↓
Test Devices
    ↓
Real Purchase Testing (FREE)
```

### Production
```
Developer Machine
    ↓
Release Build (Signed)
    ↓
Play Console (Production)
    ↓
Public Users
    ↓
Real Purchases (Paid)
```

---

## Key Design Decisions

### ✅ Why Plugin.InAppBilling?
- Cross-platform (Android, iOS, macOS)
- Well-maintained
- Active community
- Simple API
- Handles platform differences

### ✅ Why MVVM Pattern?
- Separation of concerns
- Testable code
- Reusable ViewModels
- Standard .NET MAUI pattern

### ✅ Why Demo Mode Toggle?
- Fast UI testing without setup
- No store account needed initially
- Easy to switch for production
- Clear development workflow

### ✅ Why Backend API?
- Centralized product management
- Server-side receipt validation
- Purchase history tracking
- Analytics and reporting
- Security

---

**This architecture provides:**
- 🎯 Clean separation of concerns
- 🔒 Security through validation
- 🧪 Easy testing (demo and real)
- 🌐 Cross-platform support
- 📊 Scalability for growth
- 🛠️ Maintainability

---

**Next Steps:** See `README_IAP.md` for setup instructions!

