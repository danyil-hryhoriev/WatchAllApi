using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface ICacheManager
    {
        Task<List<ChanelModel>> GetAllChanels();
        Task<ChanelModel> GetChanelById(string id);
        Task CreateChanel(ChanelModel chanelModel);
        Task RemoveChanelById(string id);
        Task UpdateChanel(ChanelModel chanelModel);

        Task<List<GenreModel>> GetAllGenres();
        Task<GenreModel> GetGenreById(string id);
        Task CreateGenre(GenreModel genreModel);
        Task RemoveGenreById(string id);
        Task UpdateGenre(GenreModel genreModel);
    }
}
