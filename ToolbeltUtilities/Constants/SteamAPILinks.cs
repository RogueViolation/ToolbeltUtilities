using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolbeltUtilities.Constants
{
    public class SteamAPILinks
    {
        public const string steamCustomIDResolve = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/"; //?key=085DC4B9B856F8AEA1D12421BA87924F&vanityurl=url"
        public const string steamUserGames = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/"; //?key=085DC4B9B856F8AEA1D12421BA87924F&steamid=steamid&format=json"
    }
}
