using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToolbeltUtilities.DataStructures;
using ToolbeltUtilities.IHelpers;
using System.Collections.Generic;

namespace ToolbeltUtilities.Helpers
{
    public class SteamAppHelper : ISteamAppHelper
    {
        private readonly ILogger<SteamAppHelper> _logger;
        private readonly Applist _steamApplist;
        private readonly string _appListPath = "../AppList.txt";
        private readonly string _steamAPIKey = "00A3FEFE22592B58BF7665D38F3FBEF1";
        private UriBuilder _uriBuilder;

        public SteamAppHelper(ILogger<SteamAppHelper> logger)
        {
            _logger = logger;
            _steamApplist = new Applist();
            _uriBuilder = new UriBuilder();
        }
        public Applist GetUserOwnedGames(string steamID)
        {
            _logger.LogInformation($"Getting owned games for user ID {steamID}");
            //TODO: Add logic for determining whether a custom or SteamID was provided

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); //TODO: move to config(?)
                client.Encoding = Encoding.UTF8;

                _uriBuilder = new UriBuilder("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/");
                TryAddQueryString("key", _steamAPIKey);
                TryAddQueryString("steamid", "76561198087268097");
                TryAddQueryString("format", "json");

                string str = client.DownloadString(_uriBuilder.Uri); //TODO: move to config

                if (!string.IsNullOrWhiteSpace(str))
                {
                    var userData = JsonConvert.DeserializeObject<SteamUserData>(str);
                    var appList = MapSteamUserDataAppIDsToApplistIDs(userData);

                    return appList;
                }
            }

            return new Applist();
        }

        private Applist GetAppList()
        {
            if (!File.Exists(_appListPath)) //TODO: move to config
            {
                if (!DownloadAppList())
                    return _steamApplist;
            }

            var lastChanged = DateTime.Now; //TODO: setup last changed
            int daysSinceChanged = (int)(DateTime.Now - lastChanged).TotalDays;
            if (daysSinceChanged > 10)
            {
                _logger.LogInformation("More than 10 days since last app list updated. Downloading new list.");
                if (!DownloadAppList())
                    return _steamApplist;
            }

            string json = File.ReadAllText(_appListPath);
            return JsonConvert.DeserializeObject<Applist>(json);
        }

        private bool DownloadAppList()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); //TODO: move to config(?)
                    client.Encoding = Encoding.UTF8;

                    string str = client.DownloadString(new Uri("https://api.steampowered.com/ISteamApps/GetAppList/v2")); //TODO: move to config

                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        File.WriteAllText(_appListPath, str); //TODO: move to config

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading app list. Steam might be down. {ex.Message}");
            }

            return false;
        }

        private bool CheckGameOwnership()
        {

            return true;
        }

        private void TryAddQueryString(string key, string value)
        {
            var queryString = HttpUtility.ParseQueryString(_uriBuilder.Query);
            queryString[key] = value;
            _uriBuilder.Query = queryString.ToString();
        }

        private Applist MapSteamUserDataAppIDsToApplistIDs(SteamUserData userData)
        {
            var appList = new Applist { Apps = new List<SteamApp>()};
            foreach (var game in userData.Response.Games)
            {
                appList.Apps.Add(new SteamApp { Appid = game.Appid});
            }
            return appList;        
        }
    }
}
