using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get All Product
    public async Task<IEnumerable<Product>> GetAllProductAsync()
    {
        var query = await _context.Products
            .AsNoTracking()
            .Include(c => c.Category)
            .ToListAsync();

        return query;
    }

    //Get Product By Id
    public async Task<Product> GetProductByIdAsync(int id)
    {
        var query = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return query;
    }

    //Add New Product
    public async Task<bool> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return true;
    }

    //Updated Product
    public async Task<bool> UpdateProductAsync(int id, Product product)
    {
        var query = await _context.Products.FindAsync(id);
        if (query == null) return false;

        query.Name = product.Name;
        query.Description = product.Description;
        query.Price = product.Price;
        query.StockQuantity = product.StockQuantity;
        query.ImageUrl = product.ImageUrl;
        query.CategoryId = product.CategoryId;
        query.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    //Remove Product By Id
    public async Task<bool> RemoveProductAsync(int id)
    {
        var query = await _context.Products.FindAsync(id);
        if (query == null) return false;

        _context.Remove(query);
        await _context.SaveChangesAsync();
        return true;
    }
}
