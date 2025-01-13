    using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateAsync(int id,Product product);
        Task<List<Product>> GetProductsWithCategoryAsync();
        Task<List<Product>> GetFilteredProductsAsync(ProductFilterDto filter);
    }
}
