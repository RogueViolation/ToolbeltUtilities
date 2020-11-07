using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolbeltUtilities.IHelpers;


namespace ToolbeltUtilities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherHelper _weatherHelper;
        private readonly ISteamAppHelper _steamAppHelper;
        private readonly double _maxTemp = 39;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherHelper weatherHelper, ISteamAppHelper steamAppHelper)
        {
            _logger = logger;
            _weatherHelper = weatherHelper;
            _steamAppHelper = steamAppHelper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                return SetupForecasts();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while executing GET. {e}");
                return null;
            }
        }

        private IEnumerable<WeatherForecast> SetupForecasts()
        {
            var asd = _steamAppHelper.GetUserOwnedGames("76561198087268097");
            var rng = new Random();
            foreach (var item in asd.Apps)
            {
                var temp = rng.NextDouble() * _maxTemp;
                yield return new WeatherForecast
                {
                    AppName = item.Name,
                    TemperatureC = Math.Round(temp, 1),
                    AppID = item.Appid.ToString()
                };
            }

        }
    }
}
