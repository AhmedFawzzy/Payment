using System.Net.Http.Json;
using Payment_Mobile.Models;

namespace Payment_Mobile.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7014/api/"; // Update with your API URL

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("Products");
            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching products: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Product>> GetSubscriptionProductsAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("Products/subscriptions");
            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching subscriptions: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Product>> GetOneTimeProductsAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("Products/one-time");
            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching one-time products: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<Product?> GetProductByIdAsync(string productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Product>($"Products/{productId}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching product: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> ValidatePurchaseAsync(Purchase purchase)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Purchases/validate", purchase);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error validating purchase: {ex.Message}");
            return false;
        }
    }

    public async Task<List<Purchase>> GetPurchaseHistoryAsync(string userId)
    {
        try
        {
            var purchases = await _httpClient.GetFromJsonAsync<List<Purchase>>($"Purchases/history/{userId}");
            return purchases ?? new List<Purchase>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching purchase history: {ex.Message}");
            return new List<Purchase>();
        }
    }
}

