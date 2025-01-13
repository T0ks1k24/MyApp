using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryRepository _categoryRepositories;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper, ICategoryRepository categoryRepositories)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _categoryRepositories = categoryRepositories;
        }

        //Get all categories
        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        //Get category by id
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return _mapper.Map<CategoryDto>(category);
        }

        //Add category
        public async Task<CategoryDto> AddCategroyAsync(Category category)
        {
            var addCategory = new Category
            {
                Name = category.Name,
                CreatedAt = DateTime.UtcNow,
            };

            await _categoryRepository.AddAsync(addCategory);

            var categoryResultDto = _mapper.Map<CategoryDto>(category);
            return categoryResultDto;
        }

        //Update category
        public async Task<CategoryDto> UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        //Delete category
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

    }
}
