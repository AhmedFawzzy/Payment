namespace Payment_Backend.Models;

public enum PurchaseStatus
{
    Pending,
    Completed,
    Failed,
    Refunded,
    Cancelled
}

public enum Platform
{
    Android,
    iOS,
    Web
}

public class Purchase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PurchaseStatus Status { get; set; }
    public Platform Platform { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiryDate { get; set; }
    public string? PurchaseToken { get; set; }
    public string? Receipt { get; set; }
    public bool IsAcknowledged { get; set; }
    public string? PaymentMethod { get; set; }
}

