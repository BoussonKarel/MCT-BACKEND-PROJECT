using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spellen.API.Data;
using Spellen.API.Models;

namespace Spellen.API.Repositories
{
    public interface ICategorieRepository
    {
        Task<List<Categorie>> GetCategorieen();
    }

    public class CategorieRepository : ICategorieRepository
    {
        private ISpellenContext _context;
        public CategorieRepository(ISpellenContext context)
        {
            _context = context;
        }

        public async Task<List<Categorie>> GetCategorieen()
        {
            try
            {
                return await _context.Categorieen.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
