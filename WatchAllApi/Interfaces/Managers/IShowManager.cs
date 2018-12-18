using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface IShowManager
    {
        Task<ShowModel> GetShowById(string id);
        Task<List<ShowModel>> GetAllShows();
        Task<ShowModel> InsertShow(ShowModel showModel);
        Task<ShowModel> UpdateShow(ShowModel showModel);
        Task RemoveShow(string id);
        Task<List<ShowModel>> GetTopShows();

        Task SeedDb();
    }
}
