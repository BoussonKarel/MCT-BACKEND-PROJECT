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
        Task<Game> AddGame(GameDTO game);
        Task<List<Category>> GetCategories();
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(string searchQuery = null, int? ageFrom = null, int? ageTo = null, int? playersMin = null, int? playersMax = null, Guid? categoryId = null);
        Task<List<Item>> GetItems();
        Task<Game> UpdateGame(GameDTO game);
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

        public async Task<Game> AddGame(GameDTO game)
        {
            Console.WriteLine(_mapper);
            Game newGame = _mapper.Map<Game>(game);

            newGame.GameCategories = new List<GameCategory>();
            foreach (CategoryDTO cat in game.Categories)
            {
                newGame.GameCategories.Add(new GameCategory() { CategoryId = cat.CategoryId });
            }

            return await _gameRepository.AddGame(newGame);
        }

        public async Task<Game> UpdateGame(GameDTO game)
        {
            Game updatedGame = _mapper.Map<Game>(game);

            // Update categories

            // Update items

            return await _gameRepository.UpdateGame(updatedGame);
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
