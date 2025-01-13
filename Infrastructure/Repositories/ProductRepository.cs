using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilterDto filter)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            if (!string.IsNullOrEmpty(filter.CategoryName))
                query = query.Where(p => p.Category.Name.ToLower().Contains(filter.CategoryName.ToLower()));
            if (!string.IsNullOrEmpty(filter.SortBy))
                if (filter.SortBy.ToLower() == "price")
                    query = filter.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                else if (filter.SortBy.ToLower() == "name")
                    query = filter.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                else if (filter.SortBy.ToLower() == "id")
                    query = filter.SortDescending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);

            return await query.Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task UpdateAsync(int id, Product product)
        {
            await _context.Products.FindAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
