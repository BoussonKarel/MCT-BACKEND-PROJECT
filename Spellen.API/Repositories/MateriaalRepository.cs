using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spellen.API.Data;
using Spellen.API.Models;

namespace Spellen.API.Repositories
{
    public interface IMateriaalRepository
    {
        Task<List<Materiaal>> GetMateriaal();
    }

    public class MateriaalRepository : IMateriaalRepository
    {
        private ISpellenContext _context;
        public MateriaalRepository(ISpellenContext context)
        {
            _context = context;
        }

        public async Task<List<Materiaal>> GetMateriaal()
        {
            try
            {
                return await _context.Materiaal.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
