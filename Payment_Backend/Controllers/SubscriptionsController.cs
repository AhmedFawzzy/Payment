using Microsoft.AspNetCore.Mvc;
using Payment_Backend.Models;
using Payment_Backend.Services;

namespace Payment_Backend.Controllers;

/// <summary>
/// Manages user subscriptions including status, history, and cancellation
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    /// <summary>
    /// Get the active subscription for a user
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>The active subscription details</returns>
    /// <response code="200">Returns the active subscription</response>
    /// <response code="404">No active subscription found</response>
    [HttpGet("active/{userId}")]
    [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveSubscription(string userId)
    {
        var subscription = await _subscriptionService.GetActiveSubscriptionAsync(userId);
        if (subscription == null)
            return NotFound(new { message = "No active subscription found" });
        
        return Ok(subscription);
    }

    /// <summary>
    /// Get subscription history for a user
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>List of all subscriptions (active and expired)</returns>
    /// <response code="200">Returns the subscription history</response>
    [HttpGet("history/{userId}")]
    [ProducesResponseType(typeof(List<Subscription>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptionHistory(string userId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionHistoryAsync(userId);
        return Ok(subscriptions);
    }

    /// <summary>
    /// Cancel a subscription (stops auto-renewal)
    /// </summary>
    /// <param name="subscriptionId">The subscription ID to cancel</param>
    /// <returns>Success status</returns>
    /// <response code="200">Subscription cancelled successfully</response>
    /// <response code="404">Subscription not found</response>
    [HttpPost("{subscriptionId}/cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSubscription(string subscriptionId)
    {
        var result = await _subscriptionService.CancelSubscriptionAsync(subscriptionId);
        if (!result)
            return NotFound(new { message = $"Subscription with ID '{subscriptionId}' not found" });
        
        return Ok(new { success = true, message = "Subscription cancelled successfully" });
    }
}

