using Microsoft.Extensions.Logging;
using Payment_Mobile.Services;
using Payment_Mobile.ViewModels;
using Payment_Mobile.Views;

namespace Payment_Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Register HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Register Services
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IPaymentService, PaymentService>();

            // Register ViewModels
            builder.Services.AddTransient<StoreViewModel>();
            builder.Services.AddTransient<SubscriptionDetailsViewModel>();
            builder.Services.AddTransient<PaymentConfirmationViewModel>();

            // Register Views
            builder.Services.AddTransient<StorePage>();
            builder.Services.AddTransient<SubscriptionDetailsPage>();
            builder.Services.AddTransient<PaymentConfirmationPage>();

            return builder.Build();
        }
    }
}
