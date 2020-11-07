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
using System.Net.Http;
using System.Linq;
using ToolbeltUtilities.IDataAccess;

namespace ToolbeltUtilities.Helpers
{
    public class SteamAppHelper : ISteamAppHelper
    {
        private readonly ILogger<SteamAppHelper> _logger;
        private readonly IAPIDataAccess _apiDataAccess;
        private Applist _steamApplist;
        private readonly string _appListPath = "../AppList.txt";
        private readonly string _steamAPIKey = "00A3FEFE22592B58BF7665D38F3FBEF1";
        private UriBuilder _uriBuilder;

        public SteamAppHelper(ILogger<SteamAppHelper> logger, IAPIDataAccess apiDataAccess)
        {
            _logger = logger;
            _apiDataAccess = apiDataAccess;
            _steamApplist = new Applist();
            _uriBuilder = new UriBuilder();
            SetupAppList();
        }
        public Applist GetUserOwnedGames(string steamID)
        {
            _logger.LogInformation($"Getting owned games for user ID {steamID}");
            //TODO: Add logic for determining whether a custom or SteamID was provided

            _uriBuilder = new UriBuilder("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/");
            TryAddQueryString("key", _steamAPIKey);
            TryAddQueryString("steamid", "76561198087268097");
            TryAddQueryString("format", "json");

            return MapSteamUserDataAppIDsToApplistIDs(_apiDataAccess.Get<SteamUserData>(_uriBuilder.Uri.ToString()));
        }

        private void SetupAppList()
        {
            try
            {
                var lastChanged = DateTime.Now; //TODO: setup last changed
                int daysSinceChanged = (int)(DateTime.Now - lastChanged).TotalDays;

                if (!File.Exists(_appListPath) || daysSinceChanged > 10) //TODO: move to config
                {
                    _logger.LogInformation("More than 10 days since last app list updated or list doesn't exist. Downloading new list.");
                    DownloadAppList();
                }

                string json = File.ReadAllText(_appListPath);
                _steamApplist = JsonConvert.DeserializeObject<SteamAppResult>(json).Applist;
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading app list. Steam might be down. {ex.Message}");
                _steamApplist = new Applist();
            }

        }

        private void DownloadAppList()
        {
            try
            {
                _logger.LogInformation($"Getting Steam games list");



                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); //TODO: move to config(?)
                    client.Encoding = Encoding.UTF8;

                    string str = client.DownloadString(new Uri("https://api.steampowered.com/ISteamApps/GetAppList/v2")); //TODO: move to config
                    _apiDataAccess.Get<SteamUserData>(_uriBuilder.Uri.ToString());
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        File.WriteAllText(_appListPath, str); //TODO: move to config
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading app list. Steam might be down. {ex.Message}");
            }
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
            SetupAppList();
            var appList = new Applist { Apps = new List<SteamApp>() };
            SteamApp app;
            foreach (var game in userData.Response.Games)
            {
                app = _steamApplist.Apps.FirstOrDefault(item => item.Appid == game.Appid) ?? new SteamApp { Appid = 0, Name = "Not Found"};

                appList.Apps.Add(new SteamApp
                {
                    Appid = game.Appid,
                    Name = app.Name
                });
            }
            return appList;
        }
    }
}
