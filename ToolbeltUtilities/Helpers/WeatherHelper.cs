using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolbeltUtilities.IHelpers;
using ToolbeltUtilities.DataStructures;

namespace ToolbeltUtilities.Helpers
{
    public class WeatherHelper : IWeatherHelper
    {
        public WeatherSummariesEnum SetupWeatherSummary(double temperature)
        {
            return (WeatherSummariesEnum)Math.Floor(temperature / 4) + 1;
        }
    }
}
