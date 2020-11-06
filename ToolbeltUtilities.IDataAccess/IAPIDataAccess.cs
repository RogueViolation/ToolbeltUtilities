using System;

namespace ToolbeltUtilities.IDataAccess
{
    public interface IAPIDataAccess
    {
        void Get<TResult>(object parameter, Uri uri);
    }
}
