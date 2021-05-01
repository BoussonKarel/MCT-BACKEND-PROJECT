using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Data;
using Spellen.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Spellen.API.Repositories
{
    public interface IGameRepository
    {
        Task<Game> GetGameById(Guid gameId);
        Task<List<Game>> GetGames(string searchQuery = null, int? ageFrom = null, int? ageTo = null, int? playersMin = null, int? playersMax = null, Guid? categoryId = null);
        Task<Game> AddGame(Game game);
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
            return await _context.Games.Where(g => g.GameId == gameId).SingleOrDefaultAsync();
        }

        public async Task<List<Game>> GetGames(
            string searchQuery = null,
            int? ageFrom = null,
            int? ageTo = null,
            int? playersMin = null,
            int? playersMax = null,
            Guid? categoryId = null
        ) {
            // STANDAARD QUERY
            IQueryable<Game> games = _context.Games
            .Include(g => g.Categories)
            .ThenInclude(cg => cg.Category)
            .Include(g => g.Items)
            .ThenInclude(ig => ig.Item);

            // ZOEKTERM
            if (!string.IsNullOrWhiteSpace(searchQuery))
                games = games.Where(g =>
                    g.Name.ToLower().Contains(searchQuery.Trim().ToLower()) // Zit het zoekwoord in de naam
                    || g.Explanation.ToLower().Contains(searchQuery.Trim().ToLower()) // of in de uitleg)
                );

            // LEEFTIJD
            if (ageFrom != null)
                games = games.Where(g => g.AgeFrom >= ageFrom);
            if (ageTo != null)
                games = games.Where(g => g.AgeTo <= ageTo);

            // SPELERS
            if (playersMin != null)
                games = games.Where(g => g.PlayersMin >= playersMin);
            if (playersMax != null)
                games = games.Where(g => g.PlayersMax <= playersMax);

            // CATEGORIE
            if (categoryId != null)
                games.Where(g => g.Categories.Where(c => c.CategoryId == categoryId).Count() > 0);

            return await games.ToListAsync();
        }

        public async Task<Game> AddGame(Game game) {
            await _context.Games.AddAsync(game);
            int changes = await _context.SaveChangesAsync();
            if (changes > 0) {
                return game;
            } else {
                throw new Exception("Game not saved.");
            }
        }
    }
}
