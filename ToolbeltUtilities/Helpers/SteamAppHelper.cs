using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToolbeltUtilities.DataStructures;

namespace ToolbeltUtilities.Helpers
{
    public class SteamAppHelper
    {
        private readonly ILogger<SteamAppHelper> _logger;
        private readonly Applist _steamApplist;
        private readonly string _appListPath = "../AppList.txt";
        private readonly string _steamAPIKey = "085DC4B9B856F8AEA1D12421BA87924F";

        public SteamAppHelper(ILogger<SteamAppHelper> logger)
        {
            _logger = logger;
            _steamApplist = new Applist();
        }
        public Applist GetUserOwnedGames(string steamID)
        {
            _logger.LogInformation($"Getting owned games for user ID {steamID}");

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
    }
}
