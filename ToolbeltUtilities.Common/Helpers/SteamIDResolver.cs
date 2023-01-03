using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ToolbeltUtilities.Common.DataStructures;
using ToolbeltUtilities.Common.IHelpers;
using ToolbeltUtilities.DataAccess;
using ToolbeltUtilities.IDataAccess;

namespace ToolbeltUtilities.Common.Helpers
{
    //Resolves user's Steam vanity ID to SteamID64
    public class SteamIDResolver : ISteamIDResolver
    {
        private readonly ILogger<SteamIDResolver> _logger;
        private readonly IAPIDataAccess _apiDataAccess;
        private readonly IConfigurationReader _configurationReader;
        public SteamIDResolver(ILogger<SteamIDResolver> logger, IAPIDataAccess apiDataAccess, IConfigurationReader configurationReader)
        {
            _logger = logger;
            _apiDataAccess = apiDataAccess;
            _configurationReader = configurationReader;
        }
        public string ResolveVanityID(string vanityID)
        {
            var steamIDResolverURL = new UriBuilder("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/");
            var queryString = HttpUtility.ParseQueryString(steamIDResolverURL.Query);
            var asd = _configurationReader.GetConfiguration("SteamAPI:SteamAPIKey");
            queryString["key"] = "1255A4A94607E54BC601E42E8837ED97";
            queryString["vanityurl"] = vanityID;
            steamIDResolverURL.Query = queryString.ToString();
            var steamID64 = _apiDataAccess.Get<SteamID64>(steamIDResolverURL.ToString());
            return steamID64.response.steamid;
        }
    }
}
