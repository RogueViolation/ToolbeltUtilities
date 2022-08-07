using System;
using System.Collections.Generic;
using System.Text;

namespace ToolbeltUtilities.IDataAccess
{
    public interface IConfigurationReader
    {
        string GetConfiguration(string section);
    }
}
