using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalNetwork.Services
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}