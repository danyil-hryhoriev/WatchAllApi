using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WatchAll.Api.Interfaces.Repositories;
using WatchAll.Api.Models;

namespace WatchAll.Api.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the genres
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        /// <summary>
        /// GenresController constructor
        /// </summary>
        /// <param name="genreRepository"></param>
        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Returns all genres that contains in DB
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all genres that contains in DB</response>
        [ProducesResponseType(typeof(List<GenreModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllChanels()
        {
            var genres = await _genreRepository.SelectAllAsync();
            return Ok(genres);
        }

        /// <summary>
        /// Returns genre by Id from DB
        /// </summary>
        /// <param name="id">The id of genre in DB</param>
        /// <response code="404">genre is not exists</response>
        /// <response code="400">Id is null or empty</response>
        /// <response code="200">Returns existing genre model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
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

        /// <summary>
        /// Saves the model in the database and return it
        /// </summary>
        /// <param name="genreModel">The model of the genre that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns saved genre model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
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

        /// <summary>
        /// Updates the existing model in the database and return it
        /// </summary>
        /// <param name="id">The id of genre in DB</param>
        /// <param name="genreModel">The model of genre that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns updated genre model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutChanel([FromRoute] string id, [FromBody] GenreModel genreModel)
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

        /// <summary>
        /// Delete the existing model in the database
        /// </summary>
        /// <param name="id">The id of genre in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">genre was deleted successfully</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 200)]
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

        /// <summary>
        /// Seed the database
        /// </summary>
        /// <response code="404">File with channels not found</response>
        /// <response code="200">DataBase was seeded successfully</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 200)]
        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "genres.txt");

            if (!System.IO.File.Exists(path))
            {
                return NotFound($"File with channel not found. Expected path: {path}");
            }

            var data = await System.IO.File.ReadAllLinesAsync(path, CancellationToken.None);
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
