# Payment System Implementation Guide for .NET MAUI (.NET 8/9)

## Executive Summary

This document outlines a comprehensive, step-by-step approach to implementing a cross-platform payment system in .NET MAUI that supports in-app purchases, subscriptions, and one-time purchases for both Android and iOS platforms.

---

## 1. Project Architecture & Planning

### 1.1 Architecture Overview

Establish a clean architecture with the following layers:

- **Presentation Layer**: MAUI pages and view models
- **Business Logic Layer**: Service interfaces and shared logic
- **Platform Layer**: Platform-specific implementations (Android/iOS)
- **Data Layer**: Models and DTOs for payment transactions

### 1.2 Design Principles

Apply the following architectural patterns:

- **Dependency Injection**: Register all services in MauiProgram.cs
- **Interface Segregation**: Define clear contracts for payment operations
- **Platform Abstraction**: Use conditional compilation and platform-specific implementations
- **Repository Pattern**: Abstract data access for purchase history and receipts

---

## 2. Prerequisites & Environment Setup

### 2.1 Development Environment Requirements

Prepare your development environment:

- Install Visual Studio 2022 (17.8+) with .NET MAUI workload
- Install .NET 8 or .NET 9 SDK
- Configure Android SDK (API Level 33+) with build tools
- Configure Xcode (14+) for iOS development on macOS
- Set up Android and iOS emulators/simulators for testing

### 2.2 Store Account Setup

Complete the following registrations:

**Google Play Console:**
- Create a Google Play Developer account ($25 one-time fee)
- Set up merchant account for payment processing
- Create your application listing
- Configure OAuth credentials for API access

**Apple App Store Connect:**
- Enroll in Apple Developer Program ($99/year)
- Accept paid applications agreement
- Configure banking and tax information
- Create App Store Connect API key

### 2.3 NuGet Package Selection

Research and select appropriate packages:

- **Plugin.InAppBilling** (James Montemagno's library) - Popular cross-platform option
- **Xamarin.InAppBilling** - Alternative for Android
- **StoreKit** bindings for iOS
- Consider custom implementations using native platform APIs

---

## 3. Solution Structure Setup

### 3.1 Project Organization

Create the following folder structure in your MAUI project:

- **/Services**: Business logic and service interfaces
- **/Services/Interfaces**: Abstract payment contracts
- **/Platforms/Android**: Android-specific implementations
- **/Platforms/iOS**: iOS-specific implementations
- **/Models**: Payment-related data models
- **/ViewModels**: MVVM view models for UI
- **/Views**: MAUI pages for payment UI
- **/Helpers**: Utility classes and extensions

### 3.2 Core Interfaces Definition

Define clean interfaces for:

- **IPaymentService**: Main payment orchestration
- **IPlatformBillingService**: Platform-specific billing operations
- **IProductService**: Product catalog management
- **ISubscriptionService**: Subscription-specific operations
- **IReceiptValidator**: Purchase verification
- **IPurchaseRepository**: Purchase history storage

---

## 4. Product Configuration

### 4.1 Define Product Types

Structure your product catalog:

**Consumable Products:**
- Items that can be purchased multiple times
- Examples: virtual currency, power-ups, hints
- Configure quantity tracking

**Non-Consumable Products:**
- One-time purchase items
- Examples: premium features, ad removal
- Configure ownership tracking

**Subscriptions:**
- Auto-renewable subscriptions
- Define billing periods (weekly, monthly, yearly)
- Configure grace periods and trial periods
- Set up subscription groups (iOS)

### 4.2 Product Configuration Files

Create centralized product definitions:

- Build a constants class with product IDs
- Maintain separate IDs for Android and iOS where required
- Include product metadata (name, description, price tier)
- Version control your product configurations

### 4.3 Store Console Configuration

**Google Play Console Steps:**
- Navigate to "Monetize" → "Products" → "In-app products"
- Create managed products with unique product IDs
- Set up subscriptions in "Subscriptions" section
- Configure base plans and offers for subscriptions
- Set pricing for each country/region
- Activate products for production or testing

**App Store Connect Steps:**
- Navigate to "App Store" → "In-App Purchases"
- Create in-app purchase items with unique IDs
- Configure subscription groups and levels
- Set localized names and descriptions
- Define pricing matrices across territories
- Submit for review (separate from app review)

---

## 5. Android Implementation Strategy

### 5.1 Google Play Billing Library Integration

Implement Android-specific billing:

- Add Google Play Billing Library NuGet package
- Create BillingClient instance in Android project
- Implement BillingClientStateListener for connection management
- Handle billing client connection lifecycle

### 5.2 Product Query Implementation

Build product discovery:

- Query available in-app products using QueryProductDetailsAsync
- Query subscription details separately
- Parse SkuDetails or ProductDetails responses
- Cache product information for offline access
- Handle query failures and retries

### 5.3 Purchase Flow Implementation

Create the purchase process:

- Launch billing flow using LaunchBillingFlow
- Configure BillingFlowParams with product details
- Handle purchase result in OnActivityResult
- Implement PurchasesUpdatedListener callback
- Process successful and failed purchases
- Handle user cancellation gracefully

### 5.4 Purchase Verification & Acknowledgment

Secure purchase validation:

- Retrieve purchase tokens from Play Billing
- Verify purchase signatures using public key
- Acknowledge purchases using AcknowledgePurchaseAsync
- Consume consumable products using ConsumePurchaseAsync
- Store verified purchases locally and on backend

### 5.5 Subscription Management

Handle recurring billing:

- Query active subscriptions
- Check subscription status and expiry
- Handle subscription upgrades/downgrades
- Implement grace period handling
- Manage subscription cancellation
- Support resubscription flows

### 5.6 Testing Strategy

Establish testing procedures:

- Add license testers in Google Play Console
- Use test product IDs for development
- Test with sandbox accounts
- Verify purchase flows with test payment methods
- Test refund scenarios
- Validate offline purchase restoration

---

## 6. iOS Implementation Strategy

### 6.1 StoreKit Integration

Implement iOS-specific billing:

- Add StoreKit framework references
- Create SKPaymentQueue observer
- Implement SKPaymentTransactionObserver delegate
- Add observer to payment queue on app launch
- Remove observer on app termination

### 6.2 Product Discovery

Build product catalog:

- Create SKProductsRequest with product identifiers
- Implement SKProductsRequestDelegate
- Handle ProductsRequest completion callback
- Parse SKProduct objects for display
- Cache product details for UI rendering
- Handle invalid product identifiers

### 6.3 Purchase Initiation

Start the purchase flow:

- Create SKPayment objects from SKProduct
- Add payment to SKPaymentQueue
- Handle multiple payment scenarios
- Implement queue management
- Support deferred purchases (parental approval)

### 6.4 Transaction Processing

Process payment results:

- Implement UpdatedTransactions delegate method
- Handle transaction states: Purchasing, Purchased, Failed, Restored, Deferred
- Extract transaction receipt data
- Finish transactions after processing
- Store transaction identifiers

### 6.5 Receipt Validation

Secure iOS purchases:

- Retrieve App Store receipt from app bundle
- Implement local receipt validation (basic)
- Implement server-side validation (recommended)
- Parse receipt JSON response
- Verify receipt authenticity and integrity
- Handle receipt refresh scenarios

### 6.6 Subscription Features

Manage auto-renewable subscriptions:

- Parse subscription information from receipt
- Calculate expiration dates
- Handle subscription renewal
- Implement subscription group logic
- Manage introductory offers and promotional offers
- Support subscription status checking

### 6.7 Restore Purchases

Enable purchase restoration:

- Create restore purchases button in UI
- Call RestoreCompletedTransactions on payment queue
- Process restored transactions
- Update user entitlements
- Handle restore completion callback
- Provide user feedback

### 6.8 Testing on iOS

Test thoroughly:

- Configure sandbox tester accounts in App Store Connect
- Test in sandbox environment
- Use test subscriptions (faster renewal periods)
- Test interrupted purchases
- Verify restore functionality
- Test across iOS versions

---

## 7. Cross-Platform Service Layer

### 7.1 Service Interface Implementation

Build the abstraction layer:

- Create concrete implementations of IPaymentService
- Register platform-specific services using conditional compilation
- Use dependency injection to resolve correct implementation
- Implement facade pattern for simplified API
- Handle platform differences transparently

### 7.2 Common Business Logic

Centralize shared functionality:

- Product validation logic
- Price formatting and currency handling
- Purchase state management
- Error handling and retry logic
- Analytics event tracking
- User notification management

### 7.3 State Management

Track payment state:

- Implement purchase state machine
- Track pending, completed, and failed purchases
- Maintain subscription status
- Handle purchase restoration state
- Synchronize state across app lifecycle
- Persist state to local storage

### 7.4 Error Handling Strategy

Build robust error management:

- Define custom exception types for payment errors
- Categorize errors: Network, User Cancelled, Invalid Product, etc.
- Implement retry mechanisms for transient failures
- Log errors for debugging and monitoring
- Provide user-friendly error messages
- Handle edge cases (no internet, store unavailable)

---

## 8. Receipt Validation & Security

### 8.1 Client-Side Validation

Implement basic security:

- Verify purchase signatures on Android
- Perform basic receipt parsing on iOS
- Check purchase token validity
- Validate product IDs match
- Ensure purchase hasn't been used
- Implement anti-fraud basic checks

### 8.2 Server-Side Validation (Recommended)

Build secure backend validation:

- Create backend API endpoint for receipt verification
- Send purchase tokens/receipts to your server
- Verify with Google Play Developer API
- Verify with Apple App Store API
- Store validated purchases in database
- Return validation result to app
- Implement idempotency for safety

### 8.3 Backend API Integration

Design backend architecture:

- Set up secure HTTPS endpoints
- Implement authentication/authorization
- Create purchase validation endpoints
- Build webhook handlers for server notifications
- Store purchase records in database
- Implement purchase history queries
- Create admin panel for purchase management

### 8.4 Google Play Developer API

Configure server-to-server validation:

- Enable Google Play Developer API in Google Cloud Console
- Create service account and download JSON key
- Grant service account access in Play Console
- Implement API calls to verify purchases
- Handle API rate limits and quotas
- Process subscription notifications

### 8.5 App Store Server API

Implement Apple validation:

- Generate App Store Connect API key
- Implement receipt verification API calls
- Use production or sandbox validation URL
- Parse receipt response structure
- Extract subscription and purchase data
- Handle renewal notifications

---

## 9. Data Storage & Persistence

### 9.1 Local Storage Strategy

Store purchase data locally:

- Use SQLite or Realm for structured storage
- Implement secure storage for sensitive data
- Store purchase receipts and tokens
- Cache product information
- Track purchase timestamps
- Implement data encryption for receipts

### 9.2 Purchase History Management

Build purchase tracking:

- Create purchase history data model
- Store all transaction records
- Implement query methods for history
- Support filtering and sorting
- Handle purchase restoration from history
- Sync with server when possible

### 9.3 Offline Support

Handle connectivity issues:

- Queue purchase requests when offline
- Store pending acknowledgments
- Retry failed operations on reconnection
- Validate cached data freshness
- Implement conflict resolution
- Provide offline access to purchased features

---

## 10. User Interface Implementation

### 10.1 Store Page Design

Create the shopping experience:

- Design product listing page with cards/lists
- Display product names, descriptions, and prices
- Show subscription duration and billing terms
- Include purchase buttons for each product
- Add loading indicators for async operations
- Display purchased/owned status
- Implement refresh functionality

### 10.2 Purchase Flow UI

Guide users through purchase:

- Show purchase confirmation dialog
- Display progress during transaction
- Show success/failure feedback
- Implement modal or bottom sheet for purchase
- Add cancel option at appropriate points
- Display terms and conditions
- Show privacy policy links

### 10.3 Subscription Management UI

Enable subscription control:

- Create subscription status page
- Display active subscriptions
- Show renewal dates and pricing
- Provide manage subscription buttons
- Link to platform subscription settings
- Display subscription benefits
- Show cancellation information

### 10.4 Purchase History UI

Track user transactions:

- Build purchase history list view
- Display transaction dates and amounts
- Show product names and types
- Implement receipt viewing
- Add filtering by date range
- Support search functionality
- Export purchase history option

### 10.5 Settings & Restore

Provide user controls:

- Add "Restore Purchases" button
- Include purchase management settings
- Link to customer support
- Display payment provider information
- Show active subscriptions count
- Provide clear action buttons

---

## 11. Testing Strategy

### 11.1 Unit Testing

Test business logic:

- Write unit tests for service layer methods
- Mock platform-specific dependencies
- Test purchase state transitions
- Verify error handling logic
- Test price formatting utilities
- Validate receipt parsing logic

### 11.2 Integration Testing

Test platform integration:

- Test Android billing library integration
- Test iOS StoreKit integration
- Verify purchase flows end-to-end
- Test subscription management
- Validate receipt verification
- Test restore purchases functionality

### 11.3 Sandbox Testing

Test with test accounts:

- Create multiple sandbox accounts
- Test all product types
- Verify subscription renewals
- Test cancellation flows
- Validate refund scenarios
- Test across different regions

### 11.4 Production Testing

Prepare for release:

- Perform alpha/beta testing
- Test with real payment methods (small amounts)
- Verify production receipt validation
- Test with real subscriptions
- Monitor for production issues
- Gather tester feedback

### 11.5 Edge Case Testing

Test unusual scenarios:

- Test purchase interruptions (phone calls, app switching)
- Test network failures during purchase
- Test rapid successive purchases
- Test with VPN enabled
- Test subscription upgrades/downgrades
- Test expired payment methods

---

## 12. Deployment Preparation

### 12.1 Google Play Submission

Prepare Android release:

- Create signed release APK/AAB
- Complete store listing with screenshots
- Configure in-app purchase declarations
- Set up pricing and distribution
- Submit for review
- Monitor review status

### 12.2 App Store Submission

Prepare iOS release:

- Create archive in Xcode
- Upload to App Store Connect
- Complete app metadata
- Submit in-app purchases for review first
- Submit app for review
- Answer review questions promptly

### 12.3 Privacy & Compliance

Meet legal requirements:

- Create privacy policy covering payments
- Disclose data collection practices
- Implement GDPR compliance if applicable
- Add terms of service
- Configure App Tracking Transparency (iOS)
- Implement age gates if needed

### 12.4 Analytics Integration

Track payment metrics:

- Integrate analytics SDK (Firebase, App Center)
- Track purchase events
- Monitor conversion rates
- Track subscription retention
- Log payment errors
- Create custom dashboards

---

## 13. Post-Launch Operations

### 13.1 Monitoring & Alerts

Set up observability:

- Monitor purchase success rates
- Track failed transaction rates
- Set up error alerting
- Monitor subscription churn
- Track revenue metrics
- Monitor API rate limits

### 13.2 Customer Support

Prepare support processes:

- Create FAQ for common issues
- Document refund process
- Build purchase troubleshooting guide
- Train support staff on payment flows
- Set up ticketing system
- Create escalation procedures

### 13.3 Maintenance Plan

Plan ongoing maintenance:

- Schedule regular library updates
- Monitor platform API changes
- Update for new OS versions
- Refactor based on usage patterns
- Optimize performance
- Address security vulnerabilities

### 13.4 A/B Testing

Optimize conversions:

- Test pricing strategies
- Test UI/UX variations
- Test product descriptions
- Experiment with promotional offers
- Test subscription tiers
- Measure and iterate

---

## 14. Advanced Features (Optional)

### 14.1 Promotional Offers

Implement special pricing:

- Configure introductory prices
- Set up promotional codes
- Create limited-time offers
- Implement dynamic pricing
- Configure upgrade/downgrade offers

### 14.2 Family Sharing (iOS)

Enable sharing features:

- Configure family sharing eligibility
- Handle shared purchases
- Verify family member purchases
- Update entitlements accordingly

### 14.3 Server Notifications

Handle asynchronous events:

- Implement Google Real-time Developer Notifications
- Implement Apple App Store Server Notifications
- Set up webhook endpoints
- Process subscription events
- Handle refund notifications
- Update user entitlements

### 14.4 Subscription Offers

Create targeted campaigns:

- Configure win-back offers
- Set up retention offers
- Create upgrade incentives
- Implement promotional periods
- Test offer effectiveness

---

## 15. Documentation & Knowledge Transfer

### 15.1 Code Documentation

Document implementation:

- Write XML documentation comments
- Create architecture decision records
- Document platform-specific quirks
- Maintain troubleshooting guide
- Create API reference documentation

### 15.2 User Documentation

Create end-user resources:

- Write help center articles
- Create video tutorials
- Build interactive guides
- Document subscription management
- Explain refund policies

### 15.3 Team Training

Enable team success:

- Conduct code walkthrough sessions
- Create onboarding documentation
- Document debugging procedures
- Share best practices
- Establish code review guidelines

---

## 16. Success Metrics & KPIs

Define measurement criteria:

- Purchase conversion rate
- Average revenue per user (ARPU)
- Subscription retention rate
- Churn rate
- Failed transaction rate
- Customer lifetime value (CLV)
- Time to first purchase
- Restore purchase success rate

---

## Conclusion

This implementation guide provides a comprehensive roadmap for building a production-ready payment system in .NET MAUI. Follow these steps sequentially, adapt to your specific requirements, and prioritize security and user experience throughout the development process.

Remember to stay updated with platform changes from Google and Apple, as billing APIs and requirements evolve regularly.