using System;
using System.Collections.Generic;
using System.Text;

namespace ToolbeltUtilities.Common.IHelpers
{
    public interface ISteamIDResolver
    {
        string ResolveVanityID(string vanityID);
    }
}
