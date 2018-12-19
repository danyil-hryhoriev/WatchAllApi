using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface ICacheManager
    {
        Task<List<ChannelModel>> GetAllChanels();
        Task<ChannelModel> GetChanelById(string id);
        Task CreateChanel(ChannelModel chanelModel);
        Task RemoveChanelById(string id);
        Task UpdateChanel(ChannelModel chanelModel);

        Task<List<GenreModel>> GetAllGenres();
        Task<GenreModel> GetGenreById(string id);
        Task CreateGenre(GenreModel genreModel);
        Task RemoveGenreById(string id);
        Task UpdateGenre(GenreModel genreModel);
    }
}
