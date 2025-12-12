using Payment_Mobile.ViewModels;

namespace Payment_Mobile.Views;

public partial class SubscriptionDetailsPage : ContentPage
{
    public SubscriptionDetailsPage(SubscriptionDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

