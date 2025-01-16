using Application.DTOs;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    //Get all categories
    public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
    {
        var categories = await _categoryRepository.GetAllCategoryAsync();

        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    //Get category by id
    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null) return null;

        return _mapper.Map<CategoryDto>(category);
    }

    //Add category
    public async Task<bool> AddCategroyAsync(CategoryDto categoryDto)
    {
        var addCategory = new Category
        {
            Name = categoryDto.Name,
            CreatedAt = DateTime.UtcNow,
        };

        var check = await _categoryRepository.AddCategoryAsync(addCategory);
        return check;
    }

    //Update category
    public async Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        bool check = await _categoryRepository.UpdateCategoryAsync(id, category);
        return check;
    }

    //Delete category
    public async Task<bool> RemoveCategoryAsync(int id)
    {
        bool check = await _categoryRepository.RemoveCategoryAsync(id);
        return check;
    }

}
