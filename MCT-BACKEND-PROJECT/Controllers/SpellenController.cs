using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCT_BACKEND_PROJECT.Models;
using MCT_BACKEND_PROJECT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MCT_BACKEND_PROJECT.Controllers
{
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
        public async Task<ActionResult<List<Spel>>> GetOccasions()
        {
            try {
                return new OkObjectResult(await _spellenService.GetSpellen());
            }
            catch(Exception ex) {
                return new StatusCodeResult(500);
            }
        }
    }
}
