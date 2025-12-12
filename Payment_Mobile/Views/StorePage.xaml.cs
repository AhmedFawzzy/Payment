using Payment_Mobile.ViewModels;

namespace Payment_Mobile.Views;

public partial class StorePage : ContentPage
{
    public StorePage(StoreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is StoreViewModel vm)
        {
            await vm.LoadProductsCommand.ExecuteAsync(null);
        }
    }
}

