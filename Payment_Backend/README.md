# Payment Backend API - Implementation Guide

## Overview

This is a RESTful API built with ASP.NET Core 9.0 that handles payment processing, receipt validation, and subscription management for the Payment Mobile app.

## Architecture

### Project Structure

```
Payment_Backend/
├── Models/
│   ├── Product.cs            # Product catalog model
│   ├── Purchase.cs           # Purchase transaction model
│   ├── Subscription.cs       # Subscription management model
│   └── DTOs/
│       └── PurchaseRequest.cs
├── Services/
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── IPurchaseService.cs
│   ├── PurchaseService.cs
│   ├── ISubscriptionService.cs
│   └── SubscriptionService.cs
├── Controllers/
│   ├── ProductsController.cs
│   ├── PurchasesController.cs
│   └── SubscriptionsController.cs
└── Program.cs
```

## API Endpoints

### Products

#### Get All Products
```
GET /api/products
```
Returns all available products (subscriptions and one-time purchases).

#### Get Subscriptions
```
GET /api/products/subscriptions
```
Returns only subscription products.

#### Get One-Time Products
```
GET /api/products/one-time
```
Returns only one-time purchase products.

#### Get Product by ID
```
GET /api/products/{id}
```
Returns a specific product by ID.

### Purchases

#### Validate Purchase
```
POST /api/purchases/validate
Content-Type: application/json

{
  "userId": "user123",
  "productId": "pro_yearly",
  "transactionId": "TXN-12345",
  "platform": "Android",
  "purchaseToken": "token_string",
  "receipt": "receipt_data"
}
```
Validates a purchase receipt and creates a purchase record.

#### Get Purchase History
```
GET /api/purchases/history/{userId}
```
Returns all purchases for a specific user.

#### Get Purchase by ID
```
GET /api/purchases/{purchaseId}
```
Returns a specific purchase.

#### Acknowledge Purchase
```
POST /api/purchases/{purchaseId}/acknowledge
```
Acknowledges a purchase (required for Google Play).

### Subscriptions

#### Get Active Subscription
```
GET /api/subscriptions/active/{userId}
```
Returns the active subscription for a user.

#### Get Subscription History
```
GET /api/subscriptions/history/{userId}
```
Returns all subscriptions for a user.

#### Cancel Subscription
```
POST /api/subscriptions/{subscriptionId}/cancel
```
Cancels a subscription (stops auto-renewal).

## Setup Instructions

### 1. Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- (Optional) SQL Server for production database

### 2. Configuration

Update `appsettings.json` with your configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "GooglePlay": {
    "ServiceAccountEmail": "your-service-account@developer.gserviceaccount.com",
    "PrivateKeyPath": "path/to/service-account-key.json"
  },
  "AppleAppStore": {
    "SharedSecret": "your-shared-secret",
    "BundleId": "com.yourcompany.payment"
  }
}
```

### 3. Running the API

#### Development
```bash
cd Payment_Backend
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:7071

#### Production
```bash
dotnet publish -c Release
```

## Product Configuration

The product catalog is currently stored in-memory. In production, this should be moved to a database.

### Current Products

**Subscriptions:**
- `pro_monthly`: $4.99/month
- `pro_yearly`: $49.99/year (includes 7-day free trial)

**One-Time Purchases:**
- `remove_ads`: $2.99 (non-consumable)
- `coin_pack_500`: $0.99 (consumable)
- `super_boost`: $1.99 (consumable)

## Receipt Validation

### Google Play Receipt Validation

To implement Google Play receipt validation:

1. Enable Google Play Developer API in Google Cloud Console
2. Create a service account and download the JSON key
3. Grant the service account access in Play Console
4. Install the NuGet package:
   ```bash
   dotnet add package Google.Apis.AndroidPublisher.v3
   ```
5. Implement validation in `PurchaseService.ValidateGooglePlayReceiptAsync()`

Example implementation:
```csharp
private async Task<bool> ValidateGooglePlayReceiptAsync(string packageName, string productId, string purchaseToken)
{
    var credential = GoogleCredential.FromFile("path/to/service-account-key.json")
        .CreateScoped(AndroidPublisherService.Scope.Androidpublisher);
    
    var service = new AndroidPublisherService(new BaseClientService.Initializer
    {
        HttpClientInitializer = credential
    });

    var request = service.Purchases.Products.Get(packageName, productId, purchaseToken);
    var purchase = await request.ExecuteAsync();
    
    return purchase.PurchaseState == 0; // 0 = Purchased
}
```

### Apple App Store Receipt Validation

To implement Apple receipt validation:

1. Use the verifyReceipt endpoint
2. Implement server-to-server notifications
3. Handle sandbox vs production URLs

Example implementation:
```csharp
private async Task<bool> ValidateAppleReceiptAsync(string receipt, bool useSandbox = false)
{
    var url = useSandbox 
        ? "https://sandbox.itunes.apple.com/verifyReceipt"
        : "https://buy.itunes.apple.com/verifyReceipt";
    
    var requestData = new
    {
        receipt_data = receipt,
        password = "your-shared-secret"
    };
    
    var response = await _httpClient.PostAsJsonAsync(url, requestData);
    var result = await response.Content.ReadFromJsonAsync<AppleReceiptResponse>();
    
    return result?.Status == 0;
}
```

## Database Migration

For production, replace in-memory storage with a database:

1. Install Entity Framework Core:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. Create DbContext:
   ```csharp
   public class PaymentDbContext : DbContext
   {
       public DbSet<Product> Products { get; set; }
       public DbSet<Purchase> Purchases { get; set; }
       public DbSet<Subscription> Subscriptions { get; set; }
   }
   ```

3. Add migration:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Security Considerations

1. **HTTPS**: Always use HTTPS in production
2. **Authentication**: Implement user authentication (JWT, OAuth2)
3. **Rate Limiting**: Add rate limiting to prevent abuse
4. **Input Validation**: Validate all input parameters
5. **Secrets Management**: Use Azure Key Vault or similar for secrets
6. **CORS**: Configure CORS properly for your mobile app domains

## Monitoring & Logging

Add structured logging:

```csharp
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
    config.AddApplicationInsights(); // For Azure
});
```

## Testing

### Unit Tests

```bash
dotnet add package xUnit
dotnet add package Moq
```

### Integration Tests

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Testing
```

## Deployment

### Docker

Create `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Payment_Backend.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment_Backend.dll"]
```

Build and run:
```bash
docker build -t payment-backend .
docker run -p 8080:80 payment-backend
```

### Azure App Service

```bash
az webapp up --name payment-backend --resource-group PaymentRG --runtime "DOTNETCORE:9.0"
```

## Webhooks & Server Notifications

### Google Play Real-time Developer Notifications

1. Set up Cloud Pub/Sub topic
2. Configure webhook endpoint:
   ```csharp
   [HttpPost("webhooks/google-play")]
   public async Task<IActionResult> HandleGooglePlayNotification([FromBody] PlayNotification notification)
   {
       // Handle subscription renewal, cancellation, etc.
   }
   ```

### Apple App Store Server Notifications

1. Configure notification URL in App Store Connect
2. Implement webhook handler:
   ```csharp
   [HttpPost("webhooks/app-store")]
   public async Task<IActionResult> HandleAppStoreNotification([FromBody] AppleNotification notification)
   {
       // Handle subscription events
   }
   ```

## Production Checklist

- [ ] Implement database persistence
- [ ] Add authentication and authorization
- [ ] Implement Google Play receipt validation
- [ ] Implement Apple receipt validation
- [ ] Set up server-to-server notifications
- [ ] Add rate limiting
- [ ] Configure CORS properly
- [ ] Set up logging and monitoring
- [ ] Add health check endpoints
- [ ] Configure SSL/TLS certificates
- [ ] Set up CI/CD pipeline
- [ ] Document API with Swagger/OpenAPI
- [ ] Add unit and integration tests
- [ ] Configure backup and disaster recovery

## Support

For issues or questions, refer to:
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)
- [Google Play Billing API](https://developers.google.com/android-publisher)
- [Apple App Store Server API](https://developer.apple.com/documentation/appstoreserverapi)

