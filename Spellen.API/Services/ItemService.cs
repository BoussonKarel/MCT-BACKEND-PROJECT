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
        Task DeleteItem(Guid itemId);
        Task<List<Item>> GetItems();
        Task<List<GameItem>> GetItemsOfGame(Guid gameId);
        Task UpdateItem(ItemDTO item);
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
        public async Task<List<Item>> GetItems()
        {
            return await _itemRepository.GetItems();
        }

        public async Task<Item> AddItem(ItemDTO item)
        {
            Item newItem = _mapper.Map<Item>(item);

            return await _itemRepository.AddItem(newItem);
        }

        public async Task UpdateItem(ItemDTO item)
        {
            Item itemToUpdate = _mapper.Map<Item>(item);
            await _itemRepository.UpdateItem(itemToUpdate);
        }

        public async Task DeleteItem(Guid itemId)
        {
            await _itemRepository.DeleteItem(itemId);
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
