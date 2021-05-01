using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spellen.API.Data;
using Spellen.API.Models;

namespace Spellen.API.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItems(string searchQuery = null);
    }

    public class ItemRepository : IItemRepository
    {
        private IGameContext _context;
        public ItemRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItems(string searchQuery = null)
        {
            if (string.IsNullOrWhiteSpace(searchQuery)) {
                return await _context.Items.ToListAsync();
            }
            else {
                return await _context.Items
                    .Where(i => i.Name.ToLower().Contains(searchQuery.Trim().ToLower()))
                    .ToListAsync();
            }
        }
    }
}
