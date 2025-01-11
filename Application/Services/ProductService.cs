using Application.DTOs;
using Application.Interfaces;
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
        private readonly IRepository<Product> _productRepository;
        private readonly IProductRepository _productRepositories;

        private readonly IRepository<Category> _categoryRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IProductRepository productRepositories)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productRepositories = productRepositories;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryName = _categoryRepository.GetByIdAsync(p.CategoryId).Result?.Name
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            var categoryName = (await _categoryRepository.GetByIdAsync(product.CategoryId))?.Name;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name
            };
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var category = await _categoryRepository
                .FindAsync(c => c.Name == productDto.CategoryName);

            if (category == null && !string.IsNullOrEmpty(productDto.CategoryName))
            {
                category = new Category { Name = productDto.CategoryName };
                await _categoryRepository.AddAsync(category);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                CategoryId = category?.Id ?? 0
            };

            await _productRepository.AddAsync(product);
        }

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

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetProductFiltered(ProductFilterDto filter)
        {
            var products = await _productRepositories.GetProductFiltered(filter);

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryName = _categoryRepository.GetByIdAsync(p.CategoryId).Result?.Name,
                InStock = p.StockQuantity > 0
            });

            return productDtos;
        }



    }
}
