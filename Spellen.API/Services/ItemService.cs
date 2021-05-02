using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetItems();
        Task<List<GameItem>> GetItemsOfGame(Guid gameId);
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
