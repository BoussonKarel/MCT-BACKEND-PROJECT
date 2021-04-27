using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCT_BACKEND_PROJECT.Data;
using MCT_BACKEND_PROJECT.Models;
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
            return await _context.Spellen.ToListAsync();
        }
    }
}
