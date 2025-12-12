# 🚀 Quick Start Guide

## Get Up and Running in 5 Minutes!

### Prerequisites
- ✅ Visual Studio 2022 (17.8+) with .NET MAUI workload
- ✅ .NET 9.0 SDK
- ✅ Android SDK or Xcode (for mobile testing)

## Step 1: Start the Backend (2 minutes)

Open a terminal and run:

```bash
cd Payment_Backend
dotnet restore
dotnet run
```

✅ Backend API will start at `https://localhost:7071`

You should see:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7071
```

## Step 2: Test the Backend (30 seconds)

Open your browser and navigate to:
```
https://localhost:7071/api/products
```

You should see JSON response with 5 products:
- 2 subscriptions (monthly & yearly)
- 3 one-time purchases (remove ads, coins, boost)

## Step 3: Run the Mobile App (2 minutes)

### Option A: Visual Studio 2022

1. Open `Payment.sln`
2. Set `Payment_Mobile` as startup project
3. Select your target platform (Android/iOS/Windows)
4. Press F5 to run

### Option B: Command Line

```bash
cd Payment_Mobile
dotnet restore

# For Android
dotnet build -t:Run -f net9.0-android

# For iOS (Mac only)
dotnet build -t:Run -f net9.0-ios

# For Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

## Step 4: Explore the App (30 seconds)

### You'll See:
1. **Store Page** (opens by default)
   - Toggle between "Subscriptions" and "One-Time" tabs
   - See beautiful product cards
   - Try clicking "Subscribe Now" or product prices

2. **Product Cards**
   - Yearly plan with "BEST VALUE" badge
   - Pricing and save percentages
   - Feature lists with checkmarks

3. **Interactions** (Demo Mode)
   - Click any purchase button
   - See loading states
   - Navigation between pages

## 🎯 What You Can Do Now

### ✅ Working Features (Demo Mode)
- Browse products
- Toggle between subscription/one-time views
- See beautiful UI matching your designs
- Navigate between pages
- View loading states

### ⚠️ Not Yet Functional (Requires Store Setup)
- Actual purchases (needs Google Play/App Store setup)
- Receipt validation (needs API keys)
- Purchase history (needs database)
- Restore purchases (needs store connection)

## 🔧 Quick Configuration

### Change API URL for Device Testing

If testing on a physical device, update the API URL:

**File**: `Payment_Mobile/Constants/ApiConstants.cs`

```csharp
// Replace localhost with your computer's IP address
public const string DevelopmentBaseUrl = "https://192.168.1.100:7071/api";
```

To find your IP:
- Windows: `ipconfig`
- Mac/Linux: `ifconfig`

### Test Without Backend

The app will gracefully handle API failures and show empty product lists. You can still explore the UI.

## 📱 What The App Looks Like

### Store Page (Default)
```
┌─────────────────────┐
│ Store      [Restore]│
│ ┌─────────┬────────┐│
│ │Subscr.  │One-Time││ ← Segmented Control
│ └─────────┴────────┘│
│                     │
│ 📅 Monthly Plan     │
│ $4.99/mo           │
│ [Subscribe Now]    │
│                     │
│ ⭐ BEST VALUE       │
│ ✓ Yearly Plan      │
│ $49.99/yr          │
│ Save 17%           │
│ [Start Free Trial] │
└─────────────────────┘
```

### One-Time Purchases
```
┌─────────────────────┐
│ 🚫 Remove Ads      │
│    Ad-free forever │
│              [$2.99]│
│                     │
│ 💰 Coin Pack (500) │
│    Head start      │
│              [$0.99]│
└─────────────────────┘
```

## 🎨 UI Features You'll See

✅ Modern Material Design
✅ Smooth animations
✅ Beautiful gradients and shadows
✅ Color-coded icons
✅ Best value badges
✅ Save percentage tags
✅ Feature lists with checkmarks
✅ Loading indicators
✅ Professional typography

## 🐛 Troubleshooting

### Backend Won't Start
```bash
# Check if port 7071 is in use
netstat -ano | findstr :7071

# Try a different port in launchSettings.json
```

### Mobile App Won't Connect
1. Check firewall allows connections
2. Use your machine's IP instead of localhost
3. Ensure backend is running
4. Check HTTPS certificate trust

### Build Errors
```bash
# Clean and restore
dotnet clean
dotnet restore
dotnet build
```

### MAUI Workload Issues
```bash
# Reinstall MAUI workload
dotnet workload install maui
```

## 📚 Next Steps

### For Learning
- [ ] Explore the code structure
- [ ] Read `IMPLEMENTATION_SUMMARY.md`
- [ ] Check out `Payment_Mobile/README.md`
- [ ] Review `Payment_Backend/README.md`

### For Development
- [ ] Set up Google Play Console account
- [ ] Set up App Store Connect account
- [ ] Configure real product IDs
- [ ] Implement actual receipt validation
- [ ] Add database persistence

### For Testing
- [ ] Run on physical Android device
- [ ] Run on physical iOS device
- [ ] Test all purchase flows
- [ ] Test subscription management

## 💡 Pro Tips

1. **Start Simple**: Get familiar with the UI first
2. **Test Backend**: Use browser/Postman to test API
3. **Check Logs**: Watch console output for errors
4. **Use Emulators**: Android Emulator is faster for initial testing
5. **Hot Reload**: XAML hot reload works in debug mode

## 🎉 You're Ready!

The app is fully functional in demo mode. To enable real purchases:
1. Follow store setup guides in the README files
2. Configure product IDs in store consoles
3. Add API keys for receipt validation
4. Test with sandbox accounts

## 🆘 Need Help?

- **Implementation Details**: See `IMPLEMENTATION_SUMMARY.md`
- **Backend API**: See `Payment_Backend/README.md`
- **Mobile Setup**: See `Payment_Mobile/README.md`
- **Full Guide**: See `payment_implementation_guide.md`

---

**Happy Coding! 🚀**

Built with ❤️ using .NET MAUI and ASP.NET Core

