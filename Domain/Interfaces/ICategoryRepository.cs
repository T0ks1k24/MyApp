using Domain.Entities;

namespace Domain.Interfaces;    

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoryAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task<bool> AddCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(int id, Category category);
    Task<bool> RemoveCategoryAsync(int id);
}
