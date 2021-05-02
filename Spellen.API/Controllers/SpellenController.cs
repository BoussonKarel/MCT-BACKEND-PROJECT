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

        // VARI COMBIS ARE LEFT OUT FOR NOW BECAUSE OF TIME
        // THEY CAN BE ADDED IN A NEXT UPDATE, A V2 OF THE API

        /// <summary>
        /// Get all games (with given parametes)
        /// </summary>
        [HttpGet]
        [Route("games")]
        public async Task<ActionResult<List<Game>>> GetGames(
            string search = null,
            int? age_from = null,
            int?  age_to = null,
            int?  players_min = null,
            int?  players_max = null,
            Guid? categoryId = null
        ) {
            try {
                GameParams parameters = new GameParams() {
                    SearchQuery = search,
                    AgeFrom = age_from,
                    AgeTo = age_to,
                    PlayersMin = players_min,
                    PlayersMax = players_max,
                    CategoryId = categoryId
                };

                return new OkObjectResult(await _gameService.GetGames(parameters));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Find game by id
        /// </summary>
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

        /// <summary>
        /// Add a new game
        /// </summary>
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

        /// <summary>
        /// Update an existing game
        /// </summary>
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

        /// <summary>
        /// Deletes a game
        /// </summary>
        [HttpDelete]
        [Route("games/{gameId}")]
        public async Task<ActionResult> DeleteGame(Guid gameId) {
            try {
                await _gameService.DeleteGame(gameId);
                return Ok();
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get all items (optional: search)
        /// </summary>
        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<List<Item>>> GetItems(string search)
        {
            try {
                if (string.IsNullOrWhiteSpace(search))
                    search = null;

                return new OkObjectResult(await _itemService.GetItems(search));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Find item by id
        /// </summary>
        [HttpGet]
        [Route("items/{itemId}")]
        public async Task<ActionResult<Item>> GetItemById(Guid itemId)
        {
            try {
                return new OkObjectResult(await _itemService.GetItemById(itemId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Add an item
        /// </summary>
        [HttpPost]
        [Route("items")]
        public async Task<ActionResult<Item>> AddItem(ItemDTO item) {
            try {
                return new OkObjectResult(await _itemService.AddItem(item));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        [HttpPut]
        [Route("items")]
        public async Task<ActionResult<Item>> UpdateItem(ItemDTO item) {
            try {
                Item result = await _itemService.UpdateItem(item);
                if (result == null) return NotFound();
                else return new OkObjectResult(result);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        [HttpDelete]
        [Route("items/{itemId}")]
        public async Task<ActionResult> DeleteItem(Guid itemId) {
            try {
                bool succes = await _itemService.DeleteItem(itemId);
                if (succes) return Ok();
                else return NotFound();
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get all categories (optional: search)
        /// </summary>
        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories(string search)
        {
            try {
                if (string.IsNullOrWhiteSpace(search))
                    search = null;
                return new OkObjectResult(await _categoryService.GetCategories(search));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Find category by id
        /// </summary>
        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<Category>> GetCategoryById(Guid categoryId)
        {
            try {
                return new OkObjectResult(await _categoryService.GetCategoryById(categoryId));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        [HttpPost]
        [Route("categories")]
        public async Task<ActionResult<Category>> AddCategory(CategoryDTO category) {
            try {
                return new OkObjectResult(await _categoryService.AddCategory(category));
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        [HttpPut]
        [Route("categories")]
        public async Task<ActionResult<Category>> UpdateCategory(CategoryDTO category) {
            try {
                Category result = await _categoryService.UpdateCategory(category);
                if (result == null) return NotFound();
                else return new OkObjectResult(result);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        [HttpDelete]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId) {
            try {
                bool succes = await _categoryService.DeleteCategory(categoryId);
                if (succes) return Ok();
                else return NotFound();
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get all categories of a game
        /// </summary>
        [HttpGet]
        [Route("games/{gameId}/categories")]
        public async Task<ActionResult<List<GameCategory>>> GetCategoriesOfGame(Guid gameId) {
            try {
                List<GameCategory> categories = await _categoryService.GetCategoriesOfGame(gameId);
                if (categories == null) return NotFound();
                else return new OkObjectResult(categories);
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Update (add / remove) categories of a game
        /// </summary>
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

        /// <summary>
        /// Get all items of a game
        /// </summary>
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
        
        /// <summary>
        /// Update (add / remove) items of a game
        /// </summary>
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
