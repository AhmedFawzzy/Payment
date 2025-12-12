# Payment Mobile App - Implementation Guide

## Overview

This is a cross-platform payment system built with .NET MAUI that supports in-app purchases, subscriptions, and one-time purchases for both Android and iOS platforms.

## Architecture

### Folder Structure

```
Payment_Mobile/
├── Models/                  # Data models
│   ├── Product.cs
│   ├── Purchase.cs
│   └── PurchaseResult.cs
├── Services/               # Business logic services
│   ├── IApiService.cs
│   ├── ApiService.cs
│   ├── IPaymentService.cs
│   └── PaymentService.cs
├── ViewModels/            # MVVM ViewModels
│   ├── StoreViewModel.cs
│   ├── SubscriptionDetailsViewModel.cs
│   └── PaymentConfirmationViewModel.cs
├── Views/                 # UI Pages
│   ├── StorePage.xaml
│   ├── SubscriptionDetailsPage.xaml
│   └── PaymentConfirmationPage.xaml
├── Converters/           # Value converters for data binding
│   ├── BooleanConverters.cs
│   └── ProductConverters.cs
├── Constants/            # Application constants
│   ├── ProductIds.cs
│   └── ApiConstants.cs
└── Platforms/           # Platform-specific code
    ├── Android/
    └── iOS/
```

## Features

### 1. Product Store (StorePage)
- Display subscription plans (Monthly/Yearly)
- Show one-time purchase items
- Segmented control to toggle between views
- Beautiful card-based UI with animations

### 2. Subscription Details (SubscriptionDetailsPage)
- Detailed subscription information
- Monthly/Yearly pricing toggle
- Feature list
- Free trial support

### 3. Payment Confirmation (PaymentConfirmationPage)
- Transaction success screen
- Receipt details
- Purchase history access

## Setup Instructions

### 1. Backend Configuration

Update the API URL in `Constants/ApiConstants.cs`:

```csharp
public const string DevelopmentBaseUrl = "https://your-api-url/api";
```

### 2. Product Configuration

Configure your product IDs in `Constants/ProductIds.cs` to match your store console settings:
- Google Play Console product IDs
- App Store Connect product IDs

### 3. Platform Setup

#### Android (Google Play)
1. Set up Google Play Console account
2. Create in-app products and subscriptions
3. Configure OAuth credentials
4. Add test accounts for sandbox testing
5. Update AndroidManifest.xml with billing permissions

#### iOS (App Store)
1. Enroll in Apple Developer Program
2. Create products in App Store Connect
3. Configure StoreKit configuration file for testing
4. Add sandbox tester accounts
5. Update Info.plist with required keys

### 4. Dependencies

The following NuGet packages are required:
- `Plugin.InAppBilling` (8.0.4) - Cross-platform IAP
- `CommunityToolkit.Mvvm` (8.3.2) - MVVM helpers
- `System.Net.Http.Json` (9.0.0) - HTTP client

## Testing

### Local Testing

1. Start the backend API:
```bash
cd Payment_Backend
dotnet run
```

2. Update the API URL in the mobile app if needed

3. Run the mobile app on your target platform

### Store Testing

#### Android Testing
- Use Google Play Console sandbox environment
- Add test accounts in Play Console
- Use test product IDs for development

#### iOS Testing
- Use StoreKit Configuration file for local testing
- Add sandbox accounts in App Store Connect
- Test on physical devices for full flow

## Key Components

### Services

#### ApiService
Handles all backend API communication:
- Product catalog retrieval
- Purchase validation
- Purchase history

#### PaymentService
Manages platform payment operations:
- Connection to payment stores
- Purchase initiation
- Receipt validation
- Restore purchases

### ViewModels

All ViewModels use MVVM pattern with:
- ObservableObject base class
- RelayCommand for user actions
- Property change notifications
- Async command support

### UI Components

Built with:
- Modern Material Design inspired UI
- Custom converters for dynamic styling
- Responsive layouts
- Shadow and animation effects

## Security Considerations

1. **Receipt Validation**: Always validate purchases on your backend server
2. **Token Storage**: Securely store purchase tokens and receipts
3. **API Security**: Use HTTPS and authentication for all API calls
4. **Obfuscation**: Enable code obfuscation for production builds

## Production Checklist

- [ ] Update product IDs to production values
- [ ] Configure production API URL
- [ ] Test all purchase flows on physical devices
- [ ] Implement receipt validation on backend
- [ ] Set up purchase analytics
- [ ] Create privacy policy and terms of service
- [ ] Test restore purchases functionality
- [ ] Configure app store listings
- [ ] Add crash reporting and logging
- [ ] Test subscription renewals and cancellations

## Troubleshooting

### Common Issues

1. **Plugin.InAppBilling connection failed**
   - Ensure device has Google Play Services (Android)
   - Check App Store availability (iOS)
   - Verify product IDs match store configuration

2. **Purchase validation failed**
   - Check backend API is running
   - Verify network connectivity
   - Check API URL configuration

3. **Products not loading**
   - Verify product IDs in store consoles
   - Check product activation status
   - Ensure app signature matches (Android)

## Next Steps

1. Implement server-side receipt validation
2. Add purchase history page
3. Implement subscription management
4. Add promotional codes support
5. Integrate analytics
6. Add customer support features

## Resources

- [Google Play Billing Documentation](https://developer.android.com/google/play/billing)
- [Apple StoreKit Documentation](https://developer.apple.com/storekit/)
- [Plugin.InAppBilling Documentation](https://github.com/jamesmontemagno/InAppBillingPlugin)
- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)

