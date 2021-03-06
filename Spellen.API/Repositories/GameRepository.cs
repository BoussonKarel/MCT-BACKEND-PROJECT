using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Data;
using Spellen.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Spellen.API.DTO;

namespace Spellen.API.Repositories
{
    public interface IGameRepository
    {
        Task<Game> AddGame(Game game);
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(GameParams gameParams);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid gameId);
    }

    public class GameRepository : IGameRepository
    {
        private IGameContext _context;
        public GameRepository(IGameContext context)
        {
            _context = context;
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return await _context.Games.Where(g => g.GameId == gameId).Include(g => g.GameCategories).ThenInclude(cg => cg.Category).Include(g => g.GameItems).ThenInclude(ig => ig.Item).SingleOrDefaultAsync();
        }

        public async Task<List<Game>> GetGames(GameParams gameParams)
        {
            // STANDAARD QUERY
            IQueryable<Game> gamesQuery = _context.Games
            .Include(g => g.GameCategories)
            .ThenInclude(cg => cg.Category)
            .Include(g => g.GameItems)
            .ThenInclude(ig => ig.Item);

            // ZOEKTERM
            if (!string.IsNullOrWhiteSpace(gameParams.SearchQuery))
                gamesQuery = gamesQuery.Where(g =>
                    g.Name.ToLower().Contains(gameParams.SearchQuery.Trim().ToLower()) // Zit het zoekwoord in de naam
                    || g.Explanation.ToLower().Contains(gameParams.SearchQuery.Trim().ToLower()) // of in de uitleg)
                );

            // LEEFTIJD
            if (gameParams.AgeFrom != null)
                gamesQuery = gamesQuery.Where(g => g.AgeFrom >= gameParams.AgeFrom);
            if (gameParams.AgeTo != null)
                gamesQuery = gamesQuery.Where(g => g.AgeTo <= gameParams.AgeTo);

            // SPELERS
            if (gameParams.PlayersMin != null)
                gamesQuery = gamesQuery.Where(g => g.PlayersMin >= gameParams.PlayersMin);
            if (gameParams.PlayersMax != null)
                gamesQuery = gamesQuery.Where(g => g.PlayersMax <= gameParams.PlayersMax);

            // CATEGORIE
            if (gameParams.CategoryId != null)
                gamesQuery = gamesQuery.Where(
                    // Is er 'any' categorie die overeenkomt?
                    g => g.GameCategories.Any(gc => gc.CategoryId == gameParams.CategoryId)
                );

            return await gamesQuery.ToListAsync();
        }

        public async Task<Game> AddGame(Game game)
        {
            _context.Games.Add(game);
            int changes = await _context.SaveChangesAsync();
            if (changes > 0)
            {
                return game;
            }
            else
            {
                throw new Exception("Game not saved.");
            }
        }

        public async Task UpdateGame(Game game)
        {
            _context.Games.Update(game); // Update de game
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGame(Guid gameId) {
            Game game = await _context.Games.Where(g => g.GameId == gameId).SingleOrDefaultAsync();
            if (game != null) {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
