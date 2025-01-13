using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
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

        public Task<Category> CreateProductAsync(Category category)
        {
        }

        public Task<Category> GetCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
