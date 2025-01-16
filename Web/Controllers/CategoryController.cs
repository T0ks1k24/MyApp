using Application.DTOs;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllCategories()
        //{
        //    var category = await _categoryService.GetAllCategoryAsync();
        //    return Ok(category);
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCategoryById(int id)
        //{
        //    var category = await _categoryService.GetCategoryByIdAsync(id);
        //    if (category == null) return null;

        //    return Ok(category);
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        //{
        //    var addCategrory = await _categoryService.AddCategroyAsync(category);
        //    return CreatedAtAction(nameof(GetCategoryById), new { id = addCategrory.Id }, addCategrory);
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateCategroy(CategoryDto categoryDto)
        //{
        //    var updateCategory = await _categoryService.UpdateCategoryAsync(category);
        //    return Ok(updateCategory);
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    await _categoryService.RemoveCategoryAsync(id);
        //    return Ok("Delete Category");
        //}


        
    }
}
