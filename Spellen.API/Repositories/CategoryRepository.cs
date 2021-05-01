using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spellen.API.Data;
using Spellen.API.Models;

namespace Spellen.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private IGameContext _context;
        public CategoryRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
