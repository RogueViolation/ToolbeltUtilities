using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolbeltUtilities.Common.IHelpers;
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
        private readonly ISteamIDResolver _idResolver;

        public SteamAppController(ILogger<SteamAppController> logger, ISteamAppHelper steamAppHelper, ISteamIDResolver idResolver)
        {
            _logger = logger;
            _steamAppHelper = steamAppHelper;
            _idResolver = idResolver;
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
            var collection = _steamAppHelper.GetUserOwnedGames("76561197962256447");
            if (collection.Apps.Any())
            {
                foreach (var item in collection.Apps)
                {
                    yield return new SteamApp
                    {
                        Name = item.Name,
                        Appid = item.Appid
                    };
                }
            }
            else yield break;

        }
    }
}
