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
        private readonly IChanelRepository _chanelRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly ConcurrentBag<ChanelModel> Chanels = new ConcurrentBag<ChanelModel>();
        private readonly ConcurrentBag<GenreModel> Genres = new ConcurrentBag<GenreModel>();

        public CacheManager(IChanelRepository chanelRepository, IGenreRepository genreRepository)
        {
            _chanelRepository = chanelRepository;
            _genreRepository = genreRepository;
        }

        public async Task LoadInCache()
        {
            var chanels = await _chanelRepository.SelectAllAsync();
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

        public Task<List<ChanelModel>> GetAllChanels()
        {
            return Task.Run(() => Chanels.ToList());
        }

        public Task<ChanelModel> GetChanelById(string id)
        {
            return Task.Run(() =>
                Chanels.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
        }

        public Task CreateChanel(ChanelModel chanelModel)
        {
            return Task.Run(() =>
            {
                _chanelRepository.InsertAsync(chanelModel);
                Chanels.Add(chanelModel);
            });
        }

        public Task RemoveChanelById(string id)
        {
            return Task.Run(() =>
            {
                _chanelRepository.DeleteAsync(model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));
            });
        }

        public Task UpdateChanel(ChanelModel chanelModel)
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
