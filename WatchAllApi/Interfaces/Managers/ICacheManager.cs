using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    /// <summary>
    /// Cache manager.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets all chanels.
        /// </summary>
        /// <returns>The all chanels.</returns>
        Task<List<ChannelModel>> GetAllChanels();

        /// <summary>
        /// Gets the chanel by identifier.
        /// </summary>
        /// <returns>The chanel by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task<ChannelModel> GetChanelById(string id);

        /// <summary>
        /// Creates the chanel.
        /// </summary>
        /// <returns>The chanel.</returns>
        /// <param name="chanelModel">Chanel model.</param>
        Task CreateChanel(ChannelModel chanelModel);

        /// <summary>
        /// Removes the chanel by identifier.
        /// </summary>
        /// <returns>The chanel by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task RemoveChanelById(string id);
        /// <summary>
        /// Updates the chanel.
        /// </summary>
        /// <returns>The chanel.</returns>
        /// <param name="chanelModel">Chanel model.</param>
        Task UpdateChanel(ChannelModel chanelModel);

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>The all genres.</returns>
        Task<List<GenreModel>> GetAllGenres();
        /// <summary>
        /// Gets the genre by identifier.
        /// </summary>
        /// <returns>The genre by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task<GenreModel> GetGenreById(string id);
        /// <summary>
        /// Creates the genre.
        /// </summary>
        /// <returns>The genre.</returns>
        /// <param name="genreModel">Genre model.</param>
        Task CreateGenre(GenreModel genreModel);
        /// <summary>
        /// Removes the genre by identifier.
        /// </summary>
        /// <returns>The genre by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task RemoveGenreById(string id);
        /// <summary>
        /// Updates the genre.
        /// </summary>
        /// <returns>The genre.</returns>
        /// <param name="genreModel">Genre model.</param>
        Task UpdateGenre(GenreModel genreModel);
    }
}
