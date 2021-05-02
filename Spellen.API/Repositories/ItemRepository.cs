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
        Task<Item> AddItem(Item item);
        Task<bool> DeleteItem(Guid itemId);
        Task<Item> GetItemById(Guid ItemId);
        Task<List<Item>> GetItems(string searchQuery = null);
        Task<List<GameItem>> GetItemsOfGame(Guid gameId);
        Task<Item> UpdateItem(Item item);
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
                throw new Exception("Item could not be added.");
        }

        public async Task<Item> UpdateItem(Item item)
        {
            Item existingItem = await _context.Items.Where(i => i.ItemId == item.ItemId).AsNoTracking().SingleOrDefaultAsync();
            if (existingItem != null) {
                _context.Items.Update(item);
                int changes = await _context.SaveChangesAsync();
                if (changes > 0)
                    return item;
                else
                    throw new Exception("Item could not be updated.");
            }
            else {
                return null;
            }
            
        }

        public async Task<bool> DeleteItem(Guid itemId)
        {
            Item existingItem = await _context.Items.Where(i => i.ItemId == itemId).SingleOrDefaultAsync();
            if (existingItem != null)
            {
                _context.Items.Remove(existingItem);
                int changes = await _context.SaveChangesAsync();
                if (changes > 0)
                    return true;
                else
                    throw new Exception("Item could not be deleted.");
            }
            else {
                return false;
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

        public async Task<Item> GetItemById(Guid ItemId)
        {
            return await _context.Items.Where(i => i.ItemId == ItemId).SingleOrDefaultAsync();
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
            int changesAdd = await _context.SaveChangesAsync();
            _context.GameItems.RemoveRange(categoriesToRemove);
            int changesRemove = await _context.SaveChangesAsync();

            if (changesAdd > 0 && changesRemove > 0)
                return items;
            else
                return null;
        }
    }
}
