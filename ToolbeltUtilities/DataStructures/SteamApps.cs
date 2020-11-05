using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ToolbeltUtilities.DataStructures
{
    public class SteamApp : IComparable<SteamApp>
    {
        [JsonProperty("appid")]
        public string Appid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public int CompareTo(SteamApp other)
        {
            return Name.CompareTo(other.Name);
        }
    }
    public class Applist
    {
        [JsonProperty("apps")]
        public IList<SteamApp> Apps { get; set; }
    }
}
