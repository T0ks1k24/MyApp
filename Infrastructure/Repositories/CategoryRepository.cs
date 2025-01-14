using Application.Interfaces.IRepositories;
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
    public class CategryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategryRepository(AppDbContext context)
        {
            _context = context;
        }

        //Get all
        public async Task<List<Category>> GetCategoryAsync()
        {
            var query = await _context.Categories.ToListAsync();
            return query;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var query = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return query;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var query = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (query == null) return null;

            query.Name = category.Name;
            query.CreatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return query;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var query = await _context.Categories.FindAsync(id);
            if (query == null) return false;

            _context.Remove(query);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
