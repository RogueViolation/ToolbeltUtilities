using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class SteamUserData
{
    [JsonProperty("response")]
    public Response Response { get; set; }
}

public partial class Response
{
    [JsonProperty("game_count")]
    public long GameCount { get; set; }

    [JsonProperty("games")]
    public List<Game> Games { get; set; }
}

public partial class Game
{
    [JsonProperty("appid")]
    public string Appid { get; set; }

    [JsonProperty("playtime_forever")]
    public long PlaytimeForever { get; set; }

    [JsonProperty("playtime_windows_forever")]
    public long PlaytimeWindowsForever { get; set; }

    [JsonProperty("playtime_mac_forever")]
    public long PlaytimeMacForever { get; set; }

    [JsonProperty("playtime_linux_forever")]
    public long PlaytimeLinuxForever { get; set; }

    [JsonProperty("playtime_2weeks", NullValueHandling = NullValueHandling.Ignore)]
    public long? Playtime2Weeks { get; set; }
}