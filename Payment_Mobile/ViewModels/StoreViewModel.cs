using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Payment_Mobile.Models;
using Payment_Mobile.Services;
using System.Collections.ObjectModel;

namespace Payment_Mobile.ViewModels;

public partial class StoreViewModel : ObservableObject
{
    private readonly IPaymentService _paymentService;
    private readonly IApiService _apiService;
    
    // 🎯 SET THIS TO FALSE WHEN YOU'RE READY TO TEST REAL PURCHASES
    private const bool USE_DEMO_MODE = true;

    [ObservableProperty]
    private ObservableCollection<Product> subscriptionProducts = new();

    [ObservableProperty]
    private ObservableCollection<Product> oneTimeProducts = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool showSubscriptions = true;

    public StoreViewModel(IPaymentService paymentService, IApiService apiService)
    {
        _paymentService = paymentService;
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        if (IsLoading) return;

        IsLoading = true;

        try
        {
            var subscriptions = await _apiService.GetSubscriptionProductsAsync();
            var oneTime = await _apiService.GetOneTimeProductsAsync();

            SubscriptionProducts.Clear();
            foreach (var product in subscriptions)
            {
                SubscriptionProducts.Add(product);
            }

            OneTimeProducts.Clear();
            foreach (var product in oneTime)
            {
                OneTimeProducts.Add(product);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task PurchaseProductAsync(Product product)
    {
        if (IsLoading || product == null) return;

        try
        {
            // 🎯 DEMO MODE - Shows info without real purchase
            if (USE_DEMO_MODE)
            {
                var productInfo = $"Product: {product.Name}\n" +
                                $"Price: {product.FormattedPrice}\n" +
                                $"Type: {product.Type}";

                if (product.Type == ProductType.Subscription)
                {
                    productInfo += $"\nPeriod: {product.SubscriptionPeriod}";
                    if (product.HasFreeTrial)
                    {
                        productInfo += $"\n\n🎉 {product.FreeTrialDays} day free trial included!";
                    }
                }

                await Shell.Current.DisplayAlert(
                    "Purchase Demo", 
                    $"{productInfo}\n\n✨ This is DEMO mode.\n\n" +
                    $"To enable real purchases:\n" +
                    $"1. Set USE_DEMO_MODE = false in StoreViewModel\n" +
                    $"2. Configure Google Play Console products\n" +
                    $"3. See ANDROID_IAP_SETUP_GUIDE.md for details",
                    "OK");
                return;
            }

            // 🚀 REAL PURCHASE MODE
            IsLoading = true;
            var result = await _paymentService.PurchaseAsync(product);

            if (result.Success)
            {
                await Shell.Current.DisplayAlert(
                    "Success!", 
                    $"Purchase completed!\n\n{product.Name}\n{product.FormattedPrice}", 
                    "OK");
                
                // Navigate to confirmation page if you have it
                // await Shell.Current.GoToAsync($"//PaymentConfirmationPage", new Dictionary<string, object>
                // {
                //     { "Purchase", result.Purchase! }
                // });
            }
            else
            {
                await Shell.Current.DisplayAlert("Purchase Failed", result.Message, "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Purchase error: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RestorePurchasesAsync()
    {
        if (IsLoading) return;

        IsLoading = true;

        try
        {
            var success = await _paymentService.RestorePurchasesAsync();
            
            if (success)
            {
                await Shell.Current.DisplayAlert("Success", "Purchases restored successfully!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to restore purchases", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Restore error: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ToggleView(string viewType)
    {
        ShowSubscriptions = viewType == "Subscriptions";
    }
}

