using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class CategryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategryRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get All Category
    public async Task<IEnumerable<Category>> GetAllCategoryAsync()
    {
        var query = await _context.Categories
            .AsNoTracking()
            .ToListAsync();
        return query;
    }

    //Get Category By Id
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var query = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return query;
    }

    //Add New Category
    public async Task<bool> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return true;
    }

    //Update Category
    public async Task<bool> UpdateCategoryAsync(int id, Category category)
    {
        var query = await _context.Categories.FindAsync(id);
        if (query == null) return false;

        query.Name = category.Name;
        await _context.SaveChangesAsync();
        return true;
    }

    //Remove Category
    public async Task<bool> RemoveCategoryAsync(int id)
    {
        var query = await _context.Categories.FindAsync(id);
        if (query == null) return false;

        _context.Remove(query);
        await _context.SaveChangesAsync();
        return true;
    }
}
