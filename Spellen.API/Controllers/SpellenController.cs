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
        [Route("spellen")]
        public async Task<ActionResult<List<Spel>>> GetSpellen()
        {
            try {
                return new OkObjectResult(await _spellenService.GetSpellen());
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("materiaal")]
        public async Task<ActionResult<List<Materiaal>>> GetMateriaal()
        {
            try {
                return new OkObjectResult(await _spellenService.GetMateriaal());
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("categorieen")]
        public async Task<ActionResult<List<Categorie>>> GetCategorieen()
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
