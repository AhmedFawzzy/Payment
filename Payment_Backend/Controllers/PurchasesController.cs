using Microsoft.AspNetCore.Mvc;
using Payment_Backend.Models;
using Payment_Backend.Models.DTOs;
using Payment_Backend.Services;

namespace Payment_Backend.Controllers;

/// <summary>
/// Handles purchase validation, recording, and history
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    /// <summary>
    /// Validate and record a purchase
    /// </summary>
    /// <param name="request">The purchase validation request with receipt/token</param>
    /// <returns>Purchase validation result with transaction details</returns>
    /// <response code="200">Purchase validated and recorded successfully</response>
    /// <response code="400">Purchase validation failed</response>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/purchases/validate
    ///     {
    ///        "userId": "user123",
    ///        "productId": "pro_yearly",
    ///        "transactionId": "TXN-12345",
    ///        "platform": 0,
    ///        "purchaseToken": "token_string",
    ///        "receipt": "receipt_data"
    ///     }
    /// </remarks>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(PurchaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PurchaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidatePurchase([FromBody] ValidateReceiptRequest request)
    {
        var result = await _purchaseService.ValidateAndRecordPurchaseAsync(request);
        
        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Get purchase history for a user
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>List of all purchases made by the user</returns>
    /// <response code="200">Returns the purchase history</response>
    [HttpGet("history/{userId}")]
    [ProducesResponseType(typeof(List<Purchase>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPurchaseHistory(string userId)
    {
        var purchases = await _purchaseService.GetPurchaseHistoryAsync(userId);
        return Ok(purchases);
    }

    /// <summary>
    /// Get a specific purchase by ID
    /// </summary>
    /// <param name="purchaseId">The purchase ID</param>
    /// <returns>The purchase details</returns>
    /// <response code="200">Returns the purchase</response>
    /// <response code="404">Purchase not found</response>
    [HttpGet("{purchaseId}")]
    [ProducesResponseType(typeof(Purchase), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPurchase(string purchaseId)
    {
        var purchase = await _purchaseService.GetPurchaseByIdAsync(purchaseId);
        if (purchase == null)
            return NotFound(new { message = $"Purchase with ID '{purchaseId}' not found" });
        
        return Ok(purchase);
    }

    /// <summary>
    /// Acknowledge a purchase (required for Google Play)
    /// </summary>
    /// <param name="purchaseId">The purchase ID to acknowledge</param>
    /// <returns>Success status</returns>
    /// <response code="200">Purchase acknowledged successfully</response>
    /// <response code="404">Purchase not found</response>
    [HttpPost("{purchaseId}/acknowledge")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AcknowledgePurchase(string purchaseId)
    {
        var result = await _purchaseService.AcknowledgePurchaseAsync(purchaseId);
        if (!result)
            return NotFound(new { message = $"Purchase with ID '{purchaseId}' not found" });
        
        return Ok(new { success = true, message = "Purchase acknowledged successfully" });
    }
}

