using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolbeltUtilities.Constants
{
    public class SteamAPILinks
    {
        //TODO: move to config
        public const string steamCustomIDResolve = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/"; 
        public const string steamUserGames = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/"; 
    }
}
