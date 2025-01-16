using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddCategroyAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, Category category);
        Task RemoveCategoryAsync(int id);

    }
}
