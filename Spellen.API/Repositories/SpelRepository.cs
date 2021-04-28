using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Data;
using Spellen.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Spellen.API.Repositories
{
    public interface ISpelRepository
    {
        Task<List<Spel>> GetSpellen();
    }

    public class SpelRepository : ISpelRepository
    {
        private ISpellenContext _context;
        public SpelRepository(ISpellenContext context)
        {
            _context = context;
        }

        public async Task<List<Spel>> GetSpellen()
        {
            try {
                return await _context.Spellen.Include(s => s.Categorieen).Include(s => s.Materiaal).ToListAsync();
            }
            catch(Exception ex) {
                throw(ex);
            }
        }
    }
}
