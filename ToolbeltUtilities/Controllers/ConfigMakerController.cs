using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolbeltUtilities.IHelpers;

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
        public string Post([FromBody] string jsonData)
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
