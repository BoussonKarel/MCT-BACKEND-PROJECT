using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Models;
using Spellen.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Spellen.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class SpellenController : ControllerBase
    {
        private readonly ISpellenService _spellenService;
        private readonly ILogger<SpellenController> _logger;
        public SpellenController(ILogger<SpellenController> logger, ISpellenService spellenService)
        {
            _logger = logger;
            _spellenService = spellenService;
        }

        [HttpGet]
        [Route("games")]
        public async Task<ActionResult<List<Game>>> GetGames(
            string search = "",
            string terrein = ""
        ) {
            try {
                if (string.IsNullOrWhiteSpace(search))
                    return new OkObjectResult(await _spellenService.GetSpellen());
                else
                    return new OkObjectResult(await _spellenService.SearchForSpellen(search));
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            try {
                return new OkObjectResult(await _spellenService.GetMateriaal());
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            try {
                return new OkObjectResult(await _spellenService.GetCategorieen());
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }
    }
}
