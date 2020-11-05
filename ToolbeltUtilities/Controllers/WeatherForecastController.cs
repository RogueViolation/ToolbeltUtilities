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
        private readonly double _maxTemp = 39;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherHelper weatherHelper)
        {
            _logger = logger;
            _weatherHelper = weatherHelper;
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
            var rng = new Random();
            for (int i=1; i<=5; i++)
            {
                var temp = rng.NextDouble() * _maxTemp;
                yield return new WeatherForecast 
                {
                        Date = DateTime.Now.AddDays(i).ToString("yyyy/MM/dd"),
                        TemperatureC = Math.Round(temp,1),
                        Summary = _weatherHelper.SetupWeatherSummary(temp).ToString()
                };
            }

        }
    }
}
