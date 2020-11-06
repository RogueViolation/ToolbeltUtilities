using System;

namespace ToolbeltUtilities.IDataAccess
{
    public interface IAPIDataAccess
    {
        T Get<T>(string address);
    }
}
