using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Spellen.API.DTO;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
{
    public interface IItemService
    {
        Task<Item> AddItem(ItemDTO item);
        Task<bool> DeleteItem(Guid itemId);
        Task<Item> GetItemById(Guid itemId);
        Task<List<Item>> GetItems(string searchQuery = null);
        Task<List<GameItem>> GetItemsOfGame(Guid gameId);
        Task<Item> UpdateItem(ItemDTO item);
        Task<List<GameItem>> UpdateItemsOfGame(Guid gameId, List<GameItem> items);
    }

    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;
        private IMapper _mapper;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetItems(string searchQuery = null)
        {
            return await _itemRepository.GetItems(searchQuery);
        }

        public async Task<Item> GetItemById(Guid itemId)
        {
            return await _itemRepository.GetItemById(itemId);
        }
        public async Task<Item> AddItem(ItemDTO item)
        {
            Item newItem = _mapper.Map<Item>(item);

            return await _itemRepository.AddItem(newItem);
        }

        public async Task<Item> UpdateItem(ItemDTO item)
        {
            Item itemToUpdate = _mapper.Map<Item>(item);
            Item result = await _itemRepository.UpdateItem(itemToUpdate);
            if (result == null)
                return null;
            else
                return result;
        }

        public async Task<bool> DeleteItem(Guid itemId)
        {
            return await _itemRepository.DeleteItem(itemId);
        }

        public async Task<List<GameItem>> GetItemsOfGame(Guid gameId)
        {
            return await _itemRepository.GetItemsOfGame(gameId);
        }

        public async Task<List<GameItem>> UpdateItemsOfGame(Guid gameId, List<GameItem> items)
        {
            return await _itemRepository.UpdateItemsOfGame(gameId, items);
        }
    }
}
