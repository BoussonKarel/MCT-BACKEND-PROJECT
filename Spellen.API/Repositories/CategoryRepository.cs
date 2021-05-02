using System.Net;
using System.Security.Cryptography.X509Certificates;
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
        Task<Category> AddCategory(Category category);
        Task<bool> DeleteCategory(Guid categoryId);
        Task<List<Category>> GetCategories(string searchQuery = null);
        Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId);
        Task<Category> GetCategoryById(Guid categoryId);
        Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories);
        Task<Category> UpdateCategory(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private IGameContext _context;
        public CategoryRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            int changes = await _context.SaveChangesAsync();
            if (changes > 0)
                return category;
            else
                throw new Exception("Category could not be added.");
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            Category existingCategory = await _context.Categories.Where(c => c.CategoryId == category.CategoryId).AsNoTracking().SingleOrDefaultAsync();
            if (existingCategory != null)
            {
                _context.Categories.Update(category); // Update de game
                int changes = await _context.SaveChangesAsync();
                if (changes > 0)
                    return category;
                else
                    throw new Exception("Category could not be updated.");
            }
            else {
                return null;
            }
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            Category existingCategory = await _context.Categories.Where(c => c.CategoryId == categoryId).SingleOrDefaultAsync();
            if (existingCategory != null)
            {
                _context.Categories.Remove(existingCategory);
                int changes = await _context.SaveChangesAsync();
                if (changes > 0) 
                    return true;
                else
                    throw new Exception("Category could not be deleted.");
                
            }
            else {
                return false;
            }
        }

        public async Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId)
        {
            return await _context.GameCategories.Where(gc => gc.GameId == gameId).Include(gc => gc.Category).ToListAsync();
        }

        public async Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories)
        {
            foreach (GameCategory gc in categories)
            {
                gc.GameId = gameId;
            }

            // Categorieën voor die game ophalen
            List<GameCategory> existingCategories = await _context.GameCategories.Where(gc => gc.GameId == gameId).ToListAsync();

            // Nog geen categorieën? Nieuwe lijst maken
            if (existingCategories == null) existingCategories = new List<GameCategory>();

            List<GameCategory> categoriesToAdd = new List<GameCategory>();
            // Items in game.GameCategories die niet voorkomen in existingCategories
            foreach (GameCategory category in categories)
            {
                // Komt deze categorie voor in de huidige?
                bool match = existingCategories.Any(gc => gc.CategoryId == category.CategoryId);
                // Nee? toevoegen
                if (!match) categoriesToAdd.Add(category);
            }

            List<GameCategory> categoriesToRemove = new List<GameCategory>();
            // Items in game.GameCategories die niet voorkomen in existingCategories
            foreach (GameCategory existingCategory in existingCategories)
            {
                // Komt deze categorie voor in de nieuwe??
                bool match = categories.Any(gc => gc.CategoryId == existingCategory.CategoryId);
                // Nee? toevegen (om te verwijderen)
                if (!match) categoriesToRemove.Add(existingCategory);
            }

            // Update de categorieën
            _context.GameCategories.AddRange(categoriesToAdd);
            int changesAdd = await _context.SaveChangesAsync();
            _context.GameCategories.RemoveRange(categoriesToRemove);
            int changesRemove = await _context.SaveChangesAsync();

            if (changesAdd > 0 && changesRemove > 0)
                return categories;
            else
                return null;
        }

        public async Task<List<Category>> GetCategories(string searchQuery = null)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return await _context.Categories.ToListAsync();
            }
            else
            {
                return await _context.Categories
                    .Where(i => i.Name.ToLower().Contains(searchQuery.Trim().ToLower()))
                    .ToListAsync();
            }
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await _context.Categories.Where(c => c.CategoryId == categoryId).SingleOrDefaultAsync();
        }
    }
}
