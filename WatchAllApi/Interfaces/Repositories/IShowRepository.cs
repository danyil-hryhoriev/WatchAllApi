using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Repositories
{
    public interface IShowRepository: IRepositoryBase<ShowModel>
    {
        Task<List<ShowModel>> GetFirstTop(int countOfEnt);
    }
}
