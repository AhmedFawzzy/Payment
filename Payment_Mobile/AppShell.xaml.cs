using Payment_Mobile.Views;

namespace Payment_Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("StorePage", typeof(StorePage));
            Routing.RegisterRoute("SubscriptionDetailsPage", typeof(SubscriptionDetailsPage));
            Routing.RegisterRoute("PaymentConfirmationPage", typeof(PaymentConfirmationPage));
        }
    }
}
