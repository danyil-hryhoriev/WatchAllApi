using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the shows
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowManager _showManager;
        Random rand = new Random();

        /// <summary>
        /// ShowsController constructor
        /// </summary>
        /// <param name="showManager"></param>
        public ShowsController(IShowManager showManager)
        {
            _showManager = showManager;
        }

        /// <summary>
        /// Returns all channels that contains in DB
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all channels that contains in DB</response>
        [ProducesResponseType(typeof(List<ChannelModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllShows()
        {
            var show = await _showManager.GetAllShows();
            return Ok(show);
        }

        /// <summary>
        /// Returns Top-100 shows by rating
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> GetFilteredShows([FromQuery] string name, [FromQuery] int count)
        {
            var show = await _showManager.GetFilteredShows(name, count);

            return Ok(show);
        }

        /// <summary>
        /// Returns show by Id from DB
        /// </summary>
        /// <param name="id">The id of show in DB</param>
        /// <response code="404">Show is not exists</response>
        /// <response code="400">Id is null or empty</response>
        /// <response code="200">Returns existing show model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetShowById([FromRoute] string id)
        {
            var show = await _showManager.GetShowById(id);
            return Ok(show);
        }

        /// <summary>
        /// Saves the model in the database and return it
        /// </summary>
        /// <param name="showModel">The model of the show that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns saved show model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostShow([FromBody] ShowModel showModel)
        {
            var res = await _showManager.InsertShow(showModel);

            return Ok(res);
        }

        /// <summary>
        /// Updates the existing model in the database and return it
        /// </summary>
        /// <param name="id">The id of show in DB</param>
        /// <param name="showModel">The model of show that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns updated show model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutShow([FromRoute] string id, [FromBody] ShowModel showModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            var res = await _showManager.UpdateShow(showModel);

            return Ok(res);
        }

        /// <summary>
        /// Delete the existing model in the database
        /// </summary>
        /// <param name="id">The id of show in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">show was deleted successfully</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 200)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteShowById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            await _showManager.RemoveShow(id);

            return Ok();
        }

        /// <summary>
        /// Seed the database
        /// </summary>
        /// <response code="404">File with shows not found</response>
        /// <response code="200">DataBase was seeded successfully</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 200)]
        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            await _showManager.SeedDb();
            return Ok();
        }


    }
}
