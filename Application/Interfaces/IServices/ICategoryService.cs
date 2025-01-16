using Application.DTOs;

namespace Application.Interfaces.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task<bool> AddCategroyAsync(CategoryDto categoryDto);
    Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryDto);
    Task<bool> RemoveCategoryAsync(int id);
}
