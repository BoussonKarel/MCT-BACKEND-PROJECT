using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spellen.API.Data;
using Spellen.API.Models;

namespace Spellen.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId);
        Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private IGameContext _context;
        public CategoryRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId)
        {
            return await _context.GameCategories.Where(gc => gc.GameId == gameId).Include(gc => gc.Category).ToListAsync();
        }

        public async Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories)
        {
            foreach (GameCategory gc in categories) {
                gc.GameId = gameId;
            }

            // Categorieën voor die game ophalen
            List<GameCategory> existingCategories = await _context.GameCategories.Where(gc => gc.GameId == gameId).AsNoTracking().ToListAsync();
            await _context.SaveChangesAsync();

            // Nog geen categorieën? Nieuwe lijst maken
            if (existingCategories == null) existingCategories = new List<GameCategory>();

            List<GameCategory> categoriesToAdd = new List<GameCategory>();
            // Items in game.GameCategories die niet voorkomen in existingCategories
            foreach (GameCategory category in categories) {
                // Komt deze categorie voor in de huidige?
                bool match = existingCategories.Any(gc => gc.CategoryId == category.CategoryId);
                // Nee? toevoegen
                if (!match) categoriesToAdd.Add(category);
            }

            List<GameCategory> categoriesToRemove = new List<GameCategory>();
            // Items in game.GameCategories die niet voorkomen in existingCategories
            foreach (GameCategory existingCategory in existingCategories) {
                // Komt deze categorie voor in de nieuwe??
                bool match = categories.Any(gc => gc.CategoryId == existingCategory.CategoryId);
                // Nee? toevegen (om te verwijderen)
                if (!match) categoriesToRemove.Add(existingCategory);
            }

            // Update de categorieën
            IGameContext context = _context;
            context.GameCategories.AddRange(categoriesToAdd);
            await context.SaveChangesAsync();
            context.GameCategories.RemoveRange(categoriesToRemove);
            await context.SaveChangesAsync();

            return categories;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
