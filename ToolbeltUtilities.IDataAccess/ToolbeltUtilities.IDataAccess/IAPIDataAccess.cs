using System;

namespace ToolbeltUtilities.IDataAccess
{
    public interface IAPIDataAccess
    {
        public TResult Get<TResult>(object parameter, Uri uri);
    }
}
