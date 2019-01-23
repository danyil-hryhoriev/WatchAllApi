using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WatchAllApi.Enums;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;
using WatchAllApi.Models.Dto;

namespace WatchAllApi.Managers
{
    /// <summary>
    /// Managing of shows and business logic for them
    /// </summary>
    public class ShowManager : IShowManager
    {
        private readonly IShowRepository _showRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IEpisodeRepository _episodeRepository;

        /// <summary>
        /// Constructor of Show manager
        /// </summary>
        /// <param name="showRepository"></param>
        /// <param name="channelRepository"></param>
        /// <param name="genreRepository"></param>
        /// <param name="seasonRepository"></param>
        /// <param name="episodeRepository"></param>
        public ShowManager(IShowRepository showRepository, IChannelRepository channelRepository, IGenreRepository genreRepository,
            ISeasonRepository seasonRepository, IEpisodeRepository episodeRepository)
        {
            _showRepository = showRepository;
            _channelRepository = channelRepository;
            _genreRepository = genreRepository;
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
        }

        /// <summary>
        /// Returns model of show by id
        /// </summary>
        /// <param name="id">Id of existing show</param>
        /// <returns></returns>
        public async Task<ShowModel> GetShowById(string id)
        {
            var res = await _seasonRepository.FindByShowId(id);
            return await _showRepository.FindAsync(id);
        }

        /// <summary>
        /// Returns all existing shows in DB
        /// </summary>
        /// <returns></returns>
        public Task<List<ShowModel>> GetAllShows()
        {
            return _showRepository.SelectAllAsync();
        }

        /// <summary>
        /// Inserts new show in Db
        /// </summary>
        /// <param name="showModel">Model of show that will be inserted</param>
        /// <returns></returns>
        public async Task<ShowModel> InsertShow(ShowModel showModel)
        {
            await _showRepository.InsertAsync(showModel);
            return showModel;
        }

        /// <summary>
        /// Updates existing show in Db
        /// </summary>
        /// <param name="showModel">Model of show that will be updated</param>
        /// <returns></returns>
        public async Task<ShowModel> UpdateShow(ShowModel showModel)
        {
            await _showRepository.ReplaceByIdAsync(showModel.Id, showModel);
            return showModel;
        }

        /// <summary>
        /// Deletes existing show in Db
        /// </summary>
        /// <param name="id">Id of show that will be deleted</param>
        /// <returns></returns>
        public async Task RemoveShow(string id)
        {
            await _showRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Get model of show with all fields from ShowModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ShowDtoModel> GetDtoShow(ShowModel model)
        {
            var chanel = await _channelRepository.FindAsync(model.ChanelId);
            var genres = new List<GenreModel>();
            foreach (var id in model.GenresIds)
            {
                genres.Add(await _genreRepository.FindAsync(id));
            }

            var seasonsDto = new List<SeasonDtoModel>();
            var seasons = await _seasonRepository.FindByShowId(model.Id);
            foreach (var season in seasons)
            {
                var episodes = await _episodeRepository.FindBySeasonId(season.Id);
                seasonsDto.Add(SeasonModelToDto(season, episodes));
            }

            return ShowModelToDto(model, chanel, genres, seasonsDto);
        }

        /// <summary>
        /// Get model of show with all fields from ShowModel
        /// </summary>
        /// <param name="showId">Id of show</param>
        /// <returns></returns>
        public async Task<ShowDtoModel> GetDtoShow(string showId)
        {
            var show = await _showRepository.FindAsync(showId);
            return await GetDtoShow(show);
        }

        /// <summary>
        /// Returns Top-100 shows by rating
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShowModel>> GetFilteredShows(string name, int count)
        {
           return await _showRepository.GetFiltered(name, count);
        }

        /// <summary>
        /// Converting ShowModel to ShowDtoModel
        /// </summary>
        /// <param name="showModel">Show model that will be converted</param>
        /// <param name="channelModel">Chanel model</param>
        /// <param name="genreModels">Genre models</param>
        /// <param name="seasonModels">Season models</param>
        /// <returns></returns>
        private ShowDtoModel ShowModelToDto(ShowModel showModel, ChannelModel channelModel, List<GenreModel> genreModels, List<SeasonDtoModel> seasonModels)
        {
            return new ShowDtoModel
            {
                Name = showModel.Name,
                Id = showModel.Id,
                Aliases = showModel.Aliases,
                Actors = showModel.Actors,
                Chanel = channelModel,
                DayOfAir = showModel.DayOfAir,
                Description = showModel.Description,
                Duration = showModel.Duration,
                Genres = genreModels,
                ImageMedium = showModel.ImageMedium,
                ImageOriginal = showModel.ImageOriginal,
                ImdbId = showModel.ImdbId,
                PremiereDate = showModel.PremiereDate,
                Rating = showModel.Rating,
                Seasons = seasonModels,
                ShowUrl = showModel.ShowUrl,
                Status = showModel.Status,
                TheTvDbId = showModel.TheTvDbId,
                TimeOfAir = showModel.TimeOfAir
            };
        }

        /// <summary>
        /// Converting SeasonModel to ShowDtoModel
        /// </summary>
        /// <param name="seasonModel">Season model that will be converted</param>
        /// <param name="episodeModels">List of episodes</param>
        /// <returns></returns>
        private SeasonDtoModel SeasonModelToDto(SeasonModel seasonModel, List<EpisodeModel> episodeModels)
        {
            return new SeasonDtoModel
            {
                Id = seasonModel.Id,
                Description = seasonModel.Description,
                PremiereDate = seasonModel.PremiereDate,
                ShowId = seasonModel.ShowId,
                EndDate = seasonModel.EndDate,
                EpisodeQty = seasonModel.EpisodeQty,
                Episodes = episodeModels,
                Image = seasonModel.Image,
                OrderId = seasonModel.OrderId
            };
        }

        #region Seed DB

        /// <summary>
        /// Seeds DB by Data from files
        /// </summary>
        /// <returns></returns>
        public async Task SeedDb()
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "DataInit", "data"));

            var chanels = await _channelRepository.SelectAllAsync();
            foreach (var file in files)
            {
                var data = await File.ReadAllLinesAsync(file, CancellationToken.None);
                var listData = data.ToList();
                var genres = await _genreRepository.SelectAllAsync();
                var listModels = listData.Select(JsonConvert.DeserializeObject<ApiLoader.Models.ShowModel>).ToList();
                foreach (var item in listModels)
                {
                    var n = await ToNormal(item, chanels, genres);
                    await _showRepository.InsertAsync(n);

                    foreach (var seasonsId in n.SeasonsIds)
                    {
                        var season = await _seasonRepository.FindAsync(seasonsId);
                        season.ShowId = n.Id;
                        await _seasonRepository.ReplaceByIdAsync(seasonsId, season);

                        foreach (var episodesId in season.EpisodesIds)
                        {
                            var episode = await _episodeRepository.FindAsync(episodesId);
                            episode.SeasonId = season.Id;
                            await _episodeRepository.ReplaceByIdAsync(episodesId, episode);
                        }
                    }
                }
            }
        }

        private async Task<ShowModel> ToNormal(ApiLoader.Models.ShowModel model, List<ChannelModel> chanels, List<GenreModel> genres)
        {
            int.TryParse(model.Duration, out var result);
            float.TryParse(model.Rating, out var doub);

            var showModel = new ShowModel();
            showModel.Name = model.Name;
            showModel.Actors = model.Actors.Select(x => new ActorModel()
            {
                Country = x.Country,
                BirthDay = x.BirthDay,
                CharacterName = x.CharacterName,
                DeathDay = x.DeathDay,
                FullName = x.FullName,
                Gender = (GenderEnum)((int)x.Gender),
                Image = x.Image,
                Role = x.Role
            }).ToList();
            showModel.Aliases = model.Aliases.Select(x => new AliasModel()
            {
                Name = x.Name,
                Region = x.Region
            }).ToList();
            showModel.ChanelId = chanels.FirstOrDefault(x =>
                string.Equals(x.Country, model.Chanel.Country, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.Name, model.Chanel.Name, StringComparison.OrdinalIgnoreCase))
                ?.Id;
            showModel.DayOfAir = string.IsNullOrEmpty(model.DayOfAir) ? DayOfWeek.Monday : Enum.Parse<DayOfWeek>(model.DayOfAir);
            showModel.Description = model.Description;
            showModel.Duration = result;
            showModel.GenresIds = model.Genres.Select(x => genres.FirstOrDefault(q => string.Equals(q.Name, x, StringComparison.OrdinalIgnoreCase))?.Id).ToList();
            showModel.ImageMedium = model.ImageMedium;
            showModel.ImageOriginal = model.ImageOriginal;
            showModel.ImdbId = model.ImdbId;
            showModel.PremiereDate = model.PremiereDate;
            showModel.Rating = doub;
            showModel.ShowUrl = model.ShowUrl;
            showModel.TheTvDbId = model.TheTvDbId;
            showModel.TimeOfAir = model.TimeOfAir;
            showModel.Status = GetStatus(model.Status);
            showModel.SeasonsIds = await GetSeasons(model.Seasons);
            return showModel;
        }

        private async Task<List<string>> GetSeasons(List<ApiLoader.Models.SeasonModel> modelSeasons)
        {
            var toRet = new List<string>();

            foreach (var model in modelSeasons)
            {
                var normaEpisodes = model.Episodes.Select(x =>
                {
                    int.TryParse(x.OrderNumber, out var order);
                    int.TryParse(x.Season, out var seas);
                    return new EpisodeModel()
                    {
                        Name = x.Name,
                        AirDate = x.AirDate,
                        Description = x.Description,
                        Duration = x.Duration,
                        OrderNumber = order,
                        Season = seas
                    };
                }).ToList();

                if (normaEpisodes.Any())
                {
                    await _episodeRepository.InsertRangeAsync(normaEpisodes);
                }


                int.TryParse(model.EpisodeQty, out var epQty);
                int.TryParse(model.OrderId, out var orderId);
                var normModel = new SeasonModel()
                {
                    EpisodesIds = normaEpisodes.Select(x => x.Id).ToList(),
                    Description = model.Description,
                    EndDate = model.EndDate,
                    EpisodeQty = epQty,
                    Image = model.Image,
                    OrderId = orderId,
                    PremiereDate = model.PremiereDate
                };

                await _seasonRepository.InsertAsync(normModel);
                toRet.Add(normModel.Id);
            }

            return toRet;
        }

        private ShowStatusEnum GetStatus(string s)
        {
            if (string.Equals(s, "Running", StringComparison.OrdinalIgnoreCase))
            {
                return ShowStatusEnum.OnAir;
            }
            else if (string.Equals(s, "Ended", StringComparison.OrdinalIgnoreCase))
            {
                return ShowStatusEnum.Ended;
            }
            else if (string.Equals(s, "In Development", StringComparison.OrdinalIgnoreCase))
            {
                return ShowStatusEnum.Survey;
            }
            else
            {
                return ShowStatusEnum.Undefined;
            }
        }

        #endregion
    }
}
