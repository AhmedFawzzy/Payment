using Payment_Mobile.ViewModels;

namespace Payment_Mobile.Views;

public partial class PaymentConfirmationPage : ContentPage
{
    public PaymentConfirmationPage(PaymentConfirmationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

