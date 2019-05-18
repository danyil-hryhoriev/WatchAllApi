using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchAll.Api.Interfaces.Managers;
using WatchAll.Api.Interfaces.Repositories;
using WatchAll.Api.Models;

namespace WatchAll.Api.Managers
{
    ///
    public class CacheManager : ICacheManager
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly ConcurrentBag<ChannelModel> Chanels = new ConcurrentBag<ChannelModel>();
        private readonly ConcurrentBag<GenreModel> Genres = new ConcurrentBag<GenreModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WatchAllApi.Managers.CacheManager"/> class.
        /// </summary>
        /// <param name="channelRepository">Channel repository.</param>
        /// <param name="genreRepository">Genre repository.</param>
        public CacheManager(IChannelRepository channelRepository, IGenreRepository genreRepository)
        {
            _channelRepository = channelRepository;
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Loads the in cache.
        /// </summary>
        /// <returns>The in cache.</returns>
        public async Task LoadInCache()
        {
            var chanels = await _channelRepository.SelectAllAsync();
            var genres = await _genreRepository.SelectAllAsync();
            foreach (var chanel in chanels)
            {
                Chanels.Add(chanel);
            }

            foreach (var genre in genres)
            {
                Genres.Add(genre);
            }
        }

        /// <summary>
        /// Gets all chanels.
        /// </summary>
        /// <returns>The all chanels.</returns>
        public Task<List<ChannelModel>> GetAllChanels()
        {
            return Task.Run(() => Chanels.ToList());
        }

        /// <summary>
        /// Gets the chanel by identifier.
        /// </summary>
        /// <returns>The chanel by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Task<ChannelModel> GetChanelById(string id)
        {
            return Task.Run(() =>
                Chanels.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Creates the chanel.
        /// </summary>
        /// <returns>The chanel.</returns>
        /// <param name="chanelModel">Chanel model.</param>
        public Task CreateChanel(ChannelModel chanelModel)
        {
            return Task.Run(() =>
            {
                _channelRepository.InsertAsync(chanelModel);
                Chanels.Add(chanelModel);
            });
        }

        /// <summary>
        /// Removes the chanel by identifier.
        /// </summary>
        /// <returns>The chanel by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Task RemoveChanelById(string id)
        {
            return Task.Run(() =>
            {
                _channelRepository.DeleteAsync(model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));
            });
        }

        /// <summary>
        /// Updates the chanel.
        /// </summary>
        /// <returns>The chanel.</returns>
        /// <param name="chanelModel">Chanel model.</param>
        public Task UpdateChanel(ChannelModel chanelModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>The all genres.</returns>
        public Task<List<GenreModel>> GetAllGenres()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the genre by identifier.
        /// </summary>
        /// <returns>The genre by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Task<GenreModel> GetGenreById(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates the genre.
        /// </summary>
        /// <returns>The genre.</returns>
        /// <param name="genreModel">Genre model.</param>
        public Task CreateGenre(GenreModel genreModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes the genre by identifier.
        /// </summary>
        /// <returns>The genre by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Task RemoveGenreById(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the genre.
        /// </summary>
        /// <returns>The genre.</returns>
        /// <param name="genreModel">Genre model.</param>
        public Task UpdateGenre(GenreModel genreModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
