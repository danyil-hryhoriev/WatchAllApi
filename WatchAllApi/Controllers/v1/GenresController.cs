using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAllApi.Interfaces;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var data = await System.IO.File.ReadAllLinesAsync(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "DataInit", "genres.txt"), CancellationToken.None);
            var listData = data.ToList();
            var listModels = listData.Select(x =>
            {
                if (string.IsNullOrEmpty(x))
                {
                    return new GenreModel() { Name = "" };
                }
                return new GenreModel() { Name = x };
            }).ToList();
            await _genreRepository.InsertRangeAsync(listModels);
            return Ok();
        }
        
    }
}
