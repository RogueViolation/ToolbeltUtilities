using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolbeltUtilities.DataStructures;
using ToolbeltUtilities.IHelpers;


namespace ToolbeltUtilities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamAppController : ControllerBase
    {
        private readonly ILogger<SteamAppController> _logger;
        private readonly ISteamAppHelper _steamAppHelper;

        public SteamAppController(ILogger<SteamAppController> logger, ISteamAppHelper steamAppHelper)
        {
            _logger = logger;
            _steamAppHelper = steamAppHelper;
        }

        [HttpGet]
        public IEnumerable<SteamApp> Get()
        {
            try
            {
                return SetupSteamApps();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing GET. {e}");
                return null;
            }
        }

        [HttpPost]
        public IActionResult PostSteamAppIDList(List<long> appIDs)
        {
            try
            {
                return new OkObjectResult(appIDs);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing POST. {e}");
                return null;
            }
        }

        private IEnumerable<SteamApp> SetupSteamApps()
        {
            var asd = _steamAppHelper.GetUserOwnedGames("76561198087268097");
            foreach (var item in asd.Apps)
            {
                yield return new SteamApp
                {
                    Name = item.Name,
                    Appid = item.Appid
                };
            }

        }
    }
}
