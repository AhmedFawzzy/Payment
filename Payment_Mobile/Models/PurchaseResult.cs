namespace Payment_Mobile.Models;

public class PurchaseResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Purchase? Purchase { get; set; }
    public string? TransactionId { get; set; }
}

