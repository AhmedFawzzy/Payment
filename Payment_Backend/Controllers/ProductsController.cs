using Microsoft.AspNetCore.Mvc;
using Payment_Backend.Services;
using Payment_Backend.Models;

namespace Payment_Backend.Controllers;

/// <summary>
/// Manages product catalog including subscriptions and one-time purchases
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Get all available products
    /// </summary>
    /// <returns>List of all products including subscriptions and one-time purchases</returns>
    /// <response code="200">Returns the list of all products</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get all subscription products
    /// </summary>
    /// <returns>List of subscription products (monthly and yearly plans)</returns>
    /// <response code="200">Returns the list of subscription products</response>
    [HttpGet("subscriptions")]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptions()
    {
        var products = await _productService.GetSubscriptionProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get all one-time purchase products
    /// </summary>
    /// <returns>List of one-time purchase products (consumables and non-consumables)</returns>
    /// <response code="200">Returns the list of one-time purchase products</response>
    [HttpGet("one-time")]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOneTimeProducts()
    {
        var products = await _productService.GetOneTimeProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get a specific product by ID
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <returns>The requested product</returns>
    /// <response code="200">Returns the product</response>
    /// <response code="404">Product not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = $"Product with ID '{id}' not found" });
        
        return Ok(product);
    }
}

