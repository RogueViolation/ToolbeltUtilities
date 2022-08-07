using System;
using System.Collections.Generic;
using System.Text;
using ToolbeltUtilities.IDataAccess;
using Microsoft.Extensions.Configuration;

namespace ToolbeltUtilities.DataAccess
{
    public class ConfigurationReader: IConfigurationReader
    {
        private readonly IConfiguration _configuration;

        public ConfigurationReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfiguration(string section)
        {
            return _configuration.GetSection(section).Value;
        }
    }
}
