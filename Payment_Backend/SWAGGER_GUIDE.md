# 📚 Swagger API Documentation Guide

## 🎯 Overview

Swagger UI is now integrated into your Payment API! It provides an interactive interface to explore and test all API endpoints.

---

## 🚀 Accessing Swagger UI

### Start the Backend

```bash
cd Payment_Backend
dotnet run
```

### Open Swagger UI

Once the backend is running, open your browser and navigate to:

**🌐 https://localhost:7014**

or

**🌐 http://localhost:5000**

The Swagger UI will open automatically at the root URL!

---

## 📋 Available Endpoints

### 🛒 Products (api/products)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products (5 total) |
| GET | `/api/products/subscriptions` | Get subscription products (2 items) |
| GET | `/api/products/one-time` | Get one-time purchases (3 items) |
| GET | `/api/products/{id}` | Get specific product by ID |

**Example Product IDs:**
- `pro_monthly` - Monthly subscription ($4.99/mo)
- `pro_yearly` - Yearly subscription ($49.99/yr)
- `remove_ads` - Remove ads ($2.99)
- `coin_pack_500` - Coin pack ($0.99)
- `super_boost` - Super boost ($1.99)

### 💳 Purchases (api/purchases)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/purchases/validate` | Validate and record a purchase |
| GET | `/api/purchases/history/{userId}` | Get purchase history for user |
| GET | `/api/purchases/{purchaseId}` | Get specific purchase |
| POST | `/api/purchases/{purchaseId}/acknowledge` | Acknowledge purchase |

### 📅 Subscriptions (api/subscriptions)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/subscriptions/active/{userId}` | Get active subscription |
| GET | `/api/subscriptions/history/{userId}` | Get subscription history |
| POST | `/api/subscriptions/{subscriptionId}/cancel` | Cancel subscription |

---

## 🧪 Testing Endpoints in Swagger

### 1. Test GET Endpoints

**Get All Products:**
1. Click on `GET /api/products`
2. Click **"Try it out"**
3. Click **"Execute"**
4. See the response with all 5 products!

### 2. Test POST Endpoints

**Validate a Purchase:**
1. Click on `POST /api/purchases/validate`
2. Click **"Try it out"**
3. Modify the request body:
```json
{
  "userId": "test_user_123",
  "productId": "pro_yearly",
  "transactionId": "TXN-TEST-001",
  "platform": 0,
  "purchaseToken": "test_token_abc123",
  "receipt": "test_receipt_data"
}
```
4. Click **"Execute"**
5. See the validation response!

### 3. Test with Parameters

**Get Product by ID:**
1. Click on `GET /api/products/{id}`
2. Click **"Try it out"**
3. Enter `pro_yearly` in the `id` field
4. Click **"Execute"**
5. See the yearly subscription details!

---

## 🎨 Swagger UI Features

### Interactive Documentation
- ✅ See all endpoints at a glance
- ✅ Detailed parameter descriptions
- ✅ Request/response examples
- ✅ HTTP status codes
- ✅ Data models/schemas

### Try It Out
- ✅ Execute requests directly from browser
- ✅ See real-time responses
- ✅ Copy cURL commands
- ✅ Download response data

### Request Duration
- ✅ See how long each request takes
- ✅ Monitor API performance

---

## 📊 Response Codes

### Success Codes
- `200 OK` - Request successful
- `201 Created` - Resource created

### Error Codes
- `400 Bad Request` - Invalid request data
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## 💡 Pro Tips

### 1. Explore the Models

At the bottom of Swagger UI, you'll find **"Schemas"** section showing:
- Product model structure
- Purchase model structure
- Subscription model structure
- Request/response DTOs

### 2. Copy cURL Commands

Each endpoint has a **cURL** button that generates a command you can run in terminal:

```bash
curl -X 'GET' \
  'https://localhost:7014/api/products' \
  -H 'accept: application/json'
```

### 3. Test Error Cases

Try invalid inputs to see error responses:
- Non-existent product ID
- Invalid user ID
- Missing required fields

### 4. Use Swagger for Development

- Test endpoints before integrating with mobile app
- Verify request/response formats
- Debug API issues
- Share API documentation with team

---

## 🔧 Customization

### Change Swagger URL

By default, Swagger opens at the root (`/`). To change this, edit `Program.cs`:

```csharp
options.RoutePrefix = "swagger"; // Now at /swagger
```

### Add Authentication

When you add authentication, update Swagger config:

```csharp
options.SwaggerDoc("v1", new OpenApiInfo
{
    // ... existing config
});

options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "JWT Authorization header",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey
});
```

---

## 📱 Testing with Mobile App

### Current Configuration

The mobile app is configured to use:
- **Development**: `https://localhost:7014/api`
- **Production**: Update in `ApiConstants.cs`

### Test Flow

1. **Start Backend** → Swagger available at root
2. **Test Endpoints** → Verify products load
3. **Start Mobile App** → Should fetch products
4. **Debug Issues** → Use Swagger to inspect responses

---

## 🎯 Quick Test Checklist

- [ ] Navigate to https://localhost:7014
- [ ] See Swagger UI with "Payment API v1"
- [ ] Expand `GET /api/products`
- [ ] Click "Try it out" and "Execute"
- [ ] Verify you see 5 products in response
- [ ] Test `GET /api/products/subscriptions`
- [ ] Verify you see 2 subscription products
- [ ] Test `GET /api/products/one-time`
- [ ] Verify you see 3 one-time products
- [ ] Try getting a product by ID: `pro_yearly`
- [ ] Verify detailed product information

---

## 📚 Additional Resources

### Swagger Documentation
- [Swashbuckle ASP.NET Core](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [OpenAPI Specification](https://swagger.io/specification/)

### API Testing Tools
- **Swagger UI** - Built-in, interactive
- **Postman** - Desktop application
- **cURL** - Command line
- **Thunder Client** - VS Code extension

---

## 🆘 Troubleshooting

### Swagger UI Not Loading

**Problem**: Page shows 404 or blank

**Solution**:
1. Verify backend is running
2. Check you're in Development mode
3. Try both HTTP and HTTPS URLs
4. Clear browser cache

### Endpoints Not Showing

**Problem**: Some endpoints missing

**Solution**:
1. Rebuild the backend
2. Restart the application
3. Check controller attributes are correct
4. Verify `[ApiController]` attribute present

### CORS Errors in Browser Console

**Problem**: CORS policy blocking requests

**Solution**: Already configured! CORS is enabled for all origins in development.

### Can't Execute Requests

**Problem**: "Try it out" button not working

**Solution**:
1. Check if backend is actually running
2. Verify port numbers match
3. Try refreshing the page
4. Check browser console for errors

---

## 🎉 You're Ready!

Swagger UI is now fully configured and ready to use! 

**Start exploring your API:**
1. Run the backend
2. Open https://localhost:7014
3. Start testing endpoints!

---

**Happy API Testing! 🚀**

