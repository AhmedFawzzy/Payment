namespace Payment_Backend.Models.DTOs;

public class PurchaseRequest
{
    public string UserId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public Platform Platform { get; set; }
    public string? PurchaseToken { get; set; }
    public string? Receipt { get; set; }
}

public class ValidateReceiptRequest
{
    public string UserId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public Platform Platform { get; set; }
    public string? PurchaseToken { get; set; }
    public string? Receipt { get; set; }
}

public class PurchaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Purchase? Purchase { get; set; }
    public Subscription? Subscription { get; set; }
}

