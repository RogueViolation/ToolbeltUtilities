using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ToolbeltUtilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigMakerController : ControllerBase
    {
        private readonly ILogger<SteamAppController> _logger;

        public ConfigMakerController(ILogger<SteamAppController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string PostSteamApp([FromBody] string steamApp)
        {
            try
            {
                return string.Empty;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing POST. {e}");
                return null;
            }
        }
    }
}
