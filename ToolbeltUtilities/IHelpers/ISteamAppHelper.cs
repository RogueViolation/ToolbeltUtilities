using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolbeltUtilities.DataStructures;

namespace ToolbeltUtilities.IHelpers
{
    public interface ISteamAppHelper
    {
        public Applist GetUserOwnedGames(string steamID);
    }
}
