using Application.DTOs;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var category = await _categoryService.GetAllCategoryAsync();
            if (category == null || category.Any())
                return NotFound("No category found!");
            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");
            return Ok(category);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
    {
        if (category == null)
            return BadRequest("Invalid category data.");

        try
        {
            var addCategrory = await _categoryService.AddCategroyAsync(category);
            if (addCategrory)
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, "Category added successfully!");

            return BadRequest("Failed to add the category.");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }

    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategroy(int id, [FromBody] CategoryDto categoryDto)
    {
        if(categoryDto == null)
            return BadRequest("Category data cannot be null.");

        try
        {
            var updateCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if(updateCategory)
                return Ok("Category updated successfully!");
            return Ok(updateCategory);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }

        
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            bool removeCategory =  await _categoryService.RemoveCategoryAsync(id);
            if(removeCategory)
                return Ok("Category removed successfully!");
            return NotFound($"Category with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
        
    }



}
