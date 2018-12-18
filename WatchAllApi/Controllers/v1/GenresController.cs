using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllChanels()
        {
            var genres = await _genreRepository.SelectAllAsync();
            return Ok(genres);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetChanelById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }
            var genres = await _genreRepository.FindAsync(id);
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> PostChanel([FromBody] GenreModel genreModel)
        {
            if (string.IsNullOrEmpty(genreModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _genreRepository.InsertAsync(genreModel);

            return Ok(genreModel);
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> PostChanel([FromRoute] string id, [FromBody] GenreModel genreModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            if (string.IsNullOrEmpty(genreModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _genreRepository.ReplaceAsync(genreModel, model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));

            return Ok(genreModel);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteChanelById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            var genres = await _genreRepository.DeleteByIdAsync(id);

            return Ok(genres);
        }


        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var data = await System.IO.File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "DataInit", "genres.txt"), CancellationToken.None);
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
