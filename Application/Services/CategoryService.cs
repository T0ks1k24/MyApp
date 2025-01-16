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

        return _mapper.Map<List<CategoryDto>>(categories);
    }

    //Get category by id
    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null) return null;

        return _mapper.Map<CategoryDto>(category);
    }

    //Add category
    public async Task<CategoryDto> AddCategroyAsync(CategoryDto categoryDto)
    {
        var addCategory = new Category
        {
            Name = categoryDto.Name,
            CreatedAt = DateTime.UtcNow,
        };

        await _categoryRepository.AddCategoryAsync(addCategory);

        return _mapper.Map<CategoryDto>(addCategory);
    }

    //Update category
    public async Task<CategoryDto> UpdateCategoryAsync(int id, Category category)
    {
        await _categoryRepository.UpdateCategoryAsync(id, category);

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

    //Delete category
    public async Task RemoveCategoryAsync(int id)
    {
        await _categoryRepository.RemoveCategoryAsync(id);
    }

}
