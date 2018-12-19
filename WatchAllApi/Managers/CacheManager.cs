using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Managers
{
    public class CacheManager : ICacheManager
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly ConcurrentBag<ChannelModel> Chanels = new ConcurrentBag<ChannelModel>();
        private readonly ConcurrentBag<GenreModel> Genres = new ConcurrentBag<GenreModel>();

        public CacheManager(IChannelRepository channelRepository, IGenreRepository genreRepository)
        {
            _channelRepository = channelRepository;
            _genreRepository = genreRepository;
        }

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

        public Task<List<ChannelModel>> GetAllChanels()
        {
            return Task.Run(() => Chanels.ToList());
        }

        public Task<ChannelModel> GetChanelById(string id)
        {
            return Task.Run(() =>
                Chanels.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
        }

        public Task CreateChanel(ChannelModel chanelModel)
        {
            return Task.Run(() =>
            {
                _channelRepository.InsertAsync(chanelModel);
                Chanels.Add(chanelModel);
            });
        }

        public Task RemoveChanelById(string id)
        {
            return Task.Run(() =>
            {
                _channelRepository.DeleteAsync(model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));
            });
        }

        public Task UpdateChanel(ChannelModel chanelModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<GenreModel>> GetAllGenres()
        {
            throw new System.NotImplementedException();
        }

        public Task<GenreModel> GetGenreById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateGenre(GenreModel genreModel)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveGenreById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateGenre(GenreModel genreModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
