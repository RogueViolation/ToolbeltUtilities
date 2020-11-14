using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolbeltUtilities.DataStructures;

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
        public IActionResult PostSteamApp(Applist appList)
        {
            try
            {
                return new OkObjectResult(appList);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing POST. {e}");
                return null;
            }
        }
    }
}
