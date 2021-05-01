using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Models;
using Spellen.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Spellen.API.DTO;

namespace Spellen.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class SpellenController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<SpellenController> _logger;
        public SpellenController(ILogger<SpellenController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet]
        [Route("games")]
        public async Task<ActionResult<List<Game>>> GetGames(
            string search = "",
            string terrein = "",
            Guid? categoryId = null
        ) {
            try {
                if (string.IsNullOrWhiteSpace(search))
                    return new OkObjectResult(await _gameService.GetGames());
                else
                    return new OkObjectResult(await _gameService.GetGames(searchQuery: search, categoryId: categoryId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("games/{gameId}")]
        public async Task<ActionResult<Game>> GetGameById(Guid gameId) {
            try {
                return new OkObjectResult(await _gameService.GetGameById(gameId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("games")]
        public async Task<ActionResult<Game>> AddGame(GameDTO game) {
            try {
                return await _gameService.AddGame(game);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("games")]
        public async Task<ActionResult<Game>> UpdateGame(GameDTO game) {
            try {
                return await _gameService.UpdateGame(game);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            try {
                return new OkObjectResult(await _gameService.GetItems());
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            try {
                return new OkObjectResult(await _gameService.GetCategories());
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }
    }
}
