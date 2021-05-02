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
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(GameParams gameParams);
        Task UpdateGame(GameUpdateDTO game);
    }

    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IMapper _mapper;

        public GameService(IMapper mapper, IGameRepository gameRepository)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return await _gameRepository.GetGameById(gameId);
        }

        public async Task<List<Game>> GetGames(GameParams gameParams)
        {
            return await _gameRepository.GetGames(gameParams);
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


    }
}
