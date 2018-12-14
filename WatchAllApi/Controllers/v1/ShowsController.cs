using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAllApi.Enums;
using WatchAllApi.Interfaces;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        private readonly IChanelRepository _chanelRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IEpisodeRepository _episodeRepository;

        public List<GenreModel> Genres { get; set; }
        public List<ChanelModel> Chanels { get; set; }
        public ShowsController(IShowRepository showRepository, IChanelRepository chanelRepository, IGenreRepository genreRepository,
                                                            ISeasonRepository seasonRepository, IEpisodeRepository episodeRepository)
        {
            _showRepository = showRepository;
            _chanelRepository = chanelRepository;
            _genreRepository = genreRepository;
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
        }


        #region SeedShows
        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var files = Directory.GetFiles(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "DataInit", "data"));

            Genres = await _genreRepository.SelectAllAsync();
            Chanels = await _chanelRepository.SelectAllAsync();
            foreach (var file in files)
            {
                var data = await System.IO.File.ReadAllLinesAsync(file, CancellationToken.None);
                var listData = data.ToList();
                Genres = await _genreRepository.SelectAllAsync();
                var listModels = listData.Select(JsonConvert.DeserializeObject<ApiLoader.Models.ShowModel>).ToList();
                var normal = new List<ShowModel>();
                foreach (var item in listModels)
                {
                    var n = await ToNormal(item);
                    await _showRepository.InsertAsync(n);
                }
            }


            return Ok();
        }

        private async Task<ShowModel> ToNormal(ApiLoader.Models.ShowModel model)
        {
            int.TryParse(model.Duration, out var result);
            double.TryParse(model.Rating, out var doub);

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
            showModel.ChanelId = Chanels.FirstOrDefault(x =>
                string.Equals(x.Country, model.Chanel.Country, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.Name, model.Chanel.Name, StringComparison.OrdinalIgnoreCase)).Id;
            showModel.DayOfAir = string.IsNullOrEmpty(model.DayOfAir) ? DayOfWeek.Monday : Enum.Parse<DayOfWeek>(model.DayOfAir);
            showModel.Description = model.Description;
            showModel.Duration = result;
            showModel.GenresIds = model.Genres.Select(x => Genres.FirstOrDefault(q => string.Equals(q.Name, x, StringComparison.OrdinalIgnoreCase)).Id).ToList();
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
