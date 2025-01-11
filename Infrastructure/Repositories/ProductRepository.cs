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

        public async Task<IEnumerable<Product>> GetProductFiltered(ProductFilterDto filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            // Перевіряємо чи правильно ініціалізовано контекст
            if (_context == null)
            {
                throw new InvalidOperationException("DbContext is not initialized.");
            }

            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.nameFilter))
                query = query.Where(p => p.Name.ToLower().Contains(filter.nameFilter.ToLower()));
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            if (filter.InStock.HasValue)
                query = query.Where(p => p.StockQuantity > 0 == filter.InStock.Value);

            if (!string.IsNullOrEmpty(filter.Sort))
            {
                switch (filter.Sort.ToLower())
                {
                    case "priceasc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "nameasc":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }

            var skip = (filter.page - 1) * filter.pageSize;
            var products = await query.Skip(skip).Take(filter.pageSize).ToListAsync();

            return products;

        }
    }
}
