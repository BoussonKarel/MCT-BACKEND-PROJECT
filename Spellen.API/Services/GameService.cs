using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Models;
using Spellen.API.Repositories;
using AutoMapper;
using Spellen.API.DTO;

namespace Spellen.API.Services
{
    public interface IGameService
    {
        Task<Game> AddGame(GameAddDTO game);
        Task DeleteGame(Guid gameId);
        Task<List<Category>> GetCategories();
        Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId);
        Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories);
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(string searchQuery = null, int? ageFrom = null, int? ageTo = null, int? playersMin = null, int? playersMax = null, Guid? categoryId = null);
        Task<List<Item>> GetItems();
        Task UpdateGame(GameUpdateDTO game);
    }

    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IItemRepository _itemRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public GameService(IMapper mapper, IGameRepository gameRepository, IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
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

        public async Task<Game> AddGame(GameAddDTO game)
        {
            Game newGame = _mapper.Map<Game>(game);

            newGame.GameCategories = new List<GameCategory>();
            foreach (CategoryDTO cat in game.Categories)
            {
                newGame.GameCategories.Add(new GameCategory() { CategoryId = cat.CategoryId });
            }

            newGame.GameItems = new List<GameItem>();
            foreach (ItemDTO item in game.Items)
            {
                newGame.GameItems.Add(new GameItem() { ItemId = item.ItemId });
            }

            return await _gameRepository.AddGame(newGame);
        }

        public async Task UpdateGame(GameUpdateDTO game) // Update de game, zonder zijn relaties
        {
            Game gameToUpdate = _mapper.Map<Game>(game);
            await _gameRepository.UpdateGame(gameToUpdate);
        }

        public async Task DeleteGame(Guid gameId)
        {
            await _gameRepository.DeleteGame(gameId);
        }

        public async Task<List<Item>> GetItems()
        {
            return await _itemRepository.GetItems();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId)
        {
            return await _categoryRepository.GetCategoriesOfGame(gameId);
        }

        public async Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories)
        {
            return await _categoryRepository.UpdateCategoriesOfGame(gameId, categories);
        }
    }
}
