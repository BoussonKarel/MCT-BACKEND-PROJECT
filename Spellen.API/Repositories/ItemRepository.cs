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
        Task<List<GameItem>> GetItemsOfGame(Guid gameId);
        Task<List<GameItem>> UpdateItemsOfGame(Guid gameId, List<GameItem> items);
    }

    public class ItemRepository : IItemRepository
    {
        private IGameContext _context;
        public ItemRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<Item> AddItem(Item item)
        {
            _context.Items.Add(item);
            int changes = await _context.SaveChangesAsync();
            if (changes > 0)
                return item;
            else
                throw new Exception("Item not saved.");
        }

        public async Task UpdateItem(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(Guid itemId) {
            Item item = await _context.Items.Where(i => i.ItemId == itemId).SingleOrDefaultAsync();
            if (item != null) {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Item>> GetItems(string searchQuery = null)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return await _context.Items.ToListAsync();
            }
            else
            {
                return await _context.Items
                    .Where(i => i.Name.ToLower().Contains(searchQuery.Trim().ToLower()))
                    .ToListAsync();
            }
        }

        public async Task<List<GameItem>> GetItemsOfGame(Guid gameId)
        {
            return await _context.GameItems.Where(gc => gc.GameId == gameId).Include(gc => gc.Item).ToListAsync();
        }

        public async Task<List<GameItem>> UpdateItemsOfGame(Guid gameId, List<GameItem> items)
        {
            foreach (GameItem gi in items)
            {
                gi.GameId = gameId;
            }

            // Items voor die game ophalen
            List<GameItem> existingItems = await _context.GameItems.Where(gc => gc.GameId == gameId).ToListAsync();

            // Nog geen items? Nieuwe lijst maken
            if (existingItems == null) existingItems = new List<GameItem>();

            List<GameItem> itemsToAdd = new List<GameItem>();
            // Items in game.GameItems die niet voorkomen in existingItems
            foreach (GameItem category in items)
            {
                // Komt dit item voor in de huidige?
                bool match = existingItems.Any(gc => gc.ItemId == category.ItemId);
                // Nee? toevoegen
                if (!match) itemsToAdd.Add(category);
            }

            List<GameItem> categoriesToRemove = new List<GameItem>();
            // Items in game.GameItems die niet voorkomen in existingItems
            foreach (GameItem existingItem in existingItems)
            {
                // Komt dit item voor in de nieuwe??
                bool match = items.Any(gc => gc.ItemId == existingItem.ItemId);
                // Nee? toevegen (om te verwijderen)
                if (!match) categoriesToRemove.Add(existingItem);
            }

            // Update de items
            _context.GameItems.AddRange(itemsToAdd);
            await _context.SaveChangesAsync();
            _context.GameItems.RemoveRange(categoriesToRemove);
            await _context.SaveChangesAsync();

            return items;
        }
    }
}
