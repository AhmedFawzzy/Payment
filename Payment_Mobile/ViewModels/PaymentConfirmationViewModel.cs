using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Payment_Mobile.Models;

namespace Payment_Mobile.ViewModels;

[QueryProperty(nameof(Purchase), "Purchase")]
public partial class PaymentConfirmationViewModel : ObservableObject
{
    [ObservableProperty]
    private Purchase? purchase;

    [RelayCommand]
    private async Task ReturnToAppAsync()
    {
        await Shell.Current.GoToAsync("//StorePage");
    }

    [RelayCommand]
    private async Task ViewPurchaseHistoryAsync()
    {
        await Shell.Current.GoToAsync("//PurchaseHistoryPage");
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await Shell.Current.GoToAsync("//StorePage");
    }
}

