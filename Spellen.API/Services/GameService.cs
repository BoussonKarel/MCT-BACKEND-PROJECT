using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
{
    public interface IGameService
    {
        Task<List<Category>> GetCategories();
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(string searchQuery = null, int? ageFrom = null, int? ageTo = null, int? playersMin = null, int? playersMax = null, Guid? categoryId = null);
        Task<List<Item>> GetItems();
    }

    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IItemRepository _itemRepository;
        private ICategoryRepository _categoryRepository;

        public GameService(IGameRepository gameRepository, IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _gameRepository = gameRepository;
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return await _gameRepository.GetGameById(gameId);
        }

        public async Task<List<Game>> GetGames(string searchQuery = null, int? ageFrom = null, int? ageTo = null, int? playersMin = null, int? playersMax = null, Guid? categoryId = null)
        {
            return await _gameRepository.GetGames(searchQuery, ageFrom, ageTo, playersMin, playersMax);
        }

        public async Task<List<Item>> GetItems()
        {
            return await _itemRepository.GetItems();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }
    }
}
