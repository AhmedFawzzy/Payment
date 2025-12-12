namespace Payment_Mobile.Constants;

public static class ApiConstants
{
    // Update these with your actual backend URLs
    public const string DevelopmentBaseUrl = "https://localhost:7014/api";
    public const string ProductionBaseUrl = "https://your-production-api.com/api";

    public static string GetBaseUrl()
    {
#if DEBUG
        return DevelopmentBaseUrl;
#else
        return ProductionBaseUrl;
#endif
    }
}

