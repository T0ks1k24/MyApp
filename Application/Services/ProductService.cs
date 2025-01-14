using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;

        private readonly IRepository<Product> _productRepository;
        private readonly IProductRepository _productRepositories;

        private readonly IRepository<Category> _categoryRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IProductRepository productRepositories, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productRepositories = productRepositories;
            _mapper = mapper;
        }

        //Get all product
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await _productRepositories.GetProductsWithCategoryAsync();
            if (products == null) return null;

            return _mapper.Map<List<ProductDto>>(products);

        }

        //Get product by id
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepositories.GetProductByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductDto>(product);
        }

        //Add product
        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            var category = await _productRepositories.GetCategoryByNameAsync(productDto.CategoryName);

            if (category == null)
            {
                category = new Category
                {
                    Name = productDto.CategoryName,
                    CreatedAt = DateTime.UtcNow
                };

                await _categoryRepository.AddAsync(category);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                ImageUrl = productDto.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                CategoryId = category.Id
            };

            await _productRepository.AddAsync(product);

            var productDtoResult = _mapper.Map<ProductDto>(product);

            return productDtoResult;
        }

        //Update product
        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);
            if (product == null) return;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;

            if (!string.IsNullOrEmpty(productDto.CategoryName))
            {
                var category = await _categoryRepository
                    .FindAsync(c => c.Name == productDto.CategoryName);

                if (category == null)
                {
                    category = new Category { Name = productDto.CategoryName };
                    await _categoryRepository.AddAsync(category);
                }

                product.CategoryId = category.Id;
            }

            await _productRepository.UpdateAsync(product);
        }

        //Remove product
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        //Get a product by filter
        public async Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filter)
        {
            if (filter == null)
                return await GetAllProductAsync();

            var products = await _productRepositories.GetFilteredProductsAsync(filter);
            return _mapper.Map<List<ProductDto>>(products);

        }

    }
}

