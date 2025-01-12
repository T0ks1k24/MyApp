﻿using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddCategroyAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<CategoryDto> UpdateCategoryAsync(Category category);

    }
}
