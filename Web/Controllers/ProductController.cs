using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetProductByFilter([FromQuery] ProductFilterDto filter)
    {
        try
        {
            var products = await _productService.GetAllProductAsync(filter);
            if (products == null || !products.Any())
                return NotFound("No products found with the given filter.");

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
        
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }

    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto addProductDto)
    {
        if (addProductDto == null)
            return BadRequest("Invalid product data.");

        try
        {
            bool addedProduct = await _productService.AddProductAsync(addProductDto);
            if (addedProduct)
                return CreatedAtAction(nameof(GetProductById), new { id = addProductDto.Id }, "Product added successfully!");

            return BadRequest("Failed to add the product.");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        if(updateProductDto ==  null)
            return BadRequest("Product data cannot be null.");

        try
        {
            bool updatedProduct = await _productService.UpdateProductAsync(id, updateProductDto);
            if (updatedProduct)
                return Ok("Product updated successfully!");

            return NotFound($"Product with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
        
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            bool removeProduct = await _productService.DeleteProductAsync(id);
            if (removeProduct)
                return Ok("Product removed successfully!");

            return NotFound($"Product with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
