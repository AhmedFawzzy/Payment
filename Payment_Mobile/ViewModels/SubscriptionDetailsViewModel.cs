using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Payment_Mobile.Models;
using Payment_Mobile.Services;

namespace Payment_Mobile.ViewModels;

[QueryProperty(nameof(Product), "Product")]
public partial class SubscriptionDetailsViewModel : ObservableObject
{
    private readonly IPaymentService _paymentService;

    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private bool isMonthlySelected;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string selectedPrice = "$49.99";

    [ObservableProperty]
    private string selectedPeriod = "year";

    public SubscriptionDetailsViewModel(IPaymentService paymentService)
    {
        _paymentService = paymentService;
        IsMonthlySelected = false; // Yearly selected by default
    }

    [RelayCommand]
    private void SelectPlan(string planType)
    {
        IsMonthlySelected = planType == "Monthly";
        
        if (IsMonthlySelected)
        {
            SelectedPrice = "$4.99";
            SelectedPeriod = "month";
        }
        else
        {
            SelectedPrice = "$49.99";
            SelectedPeriod = "year";
        }
    }

    [RelayCommand]
    private async Task StartTrialAsync()
    {
        if (IsLoading || Product == null) return;

        IsLoading = true;

        try
        {
            var result = await _paymentService.PurchaseAsync(Product);

            if (result.Success)
            {
                await Shell.Current.GoToAsync($"//PaymentConfirmationPage", new Dictionary<string, object>
                {
                    { "Purchase", result.Purchase! }
                });
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
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}

