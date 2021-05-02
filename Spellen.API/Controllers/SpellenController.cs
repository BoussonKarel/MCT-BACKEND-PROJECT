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
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<SpellenController> _logger;
        public SpellenController(ILogger<SpellenController> logger, IGameService gameService, IItemService itemService, ICategoryService categoryService)
        {
            _logger = logger;
            _gameService = gameService;
            _itemService = itemService;
            _categoryService = categoryService;
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
        public async Task<ActionResult<Game>> AddGame(GameAddDTO game) {
            try {
                return await _gameService.AddGame(game);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("games")]
        public async Task<ActionResult<GameUpdateDTO>> UpdateGame(GameUpdateDTO game) {
            try {
                // Update met gameUpdateDTO (game zonder de relaties)
                await _gameService.UpdateGame(game);
                return game;
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("games/{gameId}")]
        public async Task<ActionResult<GameUpdateDTO>> DeleteGame(Guid gameId) {
            try {
                // Update met gameUpdateDTO (game zonder de relaties)
                await _gameService.DeleteGame(gameId);
                return Ok();
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
                return new OkObjectResult(await _itemService.GetItems());
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
                return new OkObjectResult(await _categoryService.GetCategories());
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("games/{gameId}/categories")]
        public async Task<ActionResult<GameCategory>> GetCategoriesOfGame(Guid gameId) {
            try {
                return new OkObjectResult(await _categoryService.GetCategoriesOfGame(gameId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("games/{gameId}/categories")]
        public async Task<ActionResult<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories) {
            try {
                return new OkObjectResult(await _categoryService.UpdateCategoriesOfGame(gameId, categories));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("games/{gameId}/items")]
        public async Task<ActionResult<GameItem>> GetItemsOfGame(Guid gameId) {
            try {
                return new OkObjectResult(await _itemService.GetItemsOfGame(gameId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("games/{gameId}/items")]
        public async Task<ActionResult<GameItem>> UpdateItemsOfGame(Guid gameId, List<GameItem> items) {
            try {
                return new OkObjectResult(await _itemService.UpdateItemsOfGame(gameId, items));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }
    }
}
