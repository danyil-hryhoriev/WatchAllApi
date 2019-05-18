using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAll.Api.Interfaces.Repositories;
using WatchAll.Api.Models;

namespace WatchAll.Api.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the channels
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChanelsController : ControllerBase
    {
        private readonly IChannelRepository _channelRepository;

        /// <summary>
        /// ChannelsController constructor
        /// </summary>
        /// <param name="channelRepository"></param>
        public ChanelsController(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        /// <summary>
        /// Returns all channels that contains in DB
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all channels that contains in DB</response>
        [ProducesResponseType(typeof(List<ChannelModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllChanels()
        {
            var chanels = await _channelRepository.SelectAllAsync();
            return Ok(chanels);
        }

        /// <summary>
        /// Returns channel by Id from DB
        /// </summary>
        /// <param name="id">The id of channel in DB</param>
        /// <response code="404">Channel is not exists</response>
        /// <response code="400">Id is null or empty</response>
        /// <response code="200">Returns existing channel model</response>
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

            var channelModel = await _channelRepository.FindAsync(id);

            if (channelModel == null)
            {
                return NotFound("Channel is not exist");
            }

            return Ok(channelModel);
        }

        /// <summary>
        /// Saves the model in the database and return it
        /// </summary>
        /// <param name="chanelModel">The model of the channel that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns saved channel model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostChanel([FromBody] ChannelModel chanelModel)
        {
            if (string.IsNullOrEmpty(chanelModel.Country) || string.IsNullOrEmpty(chanelModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _channelRepository.InsertAsync(chanelModel);

            return Ok(chanelModel);
        }

        /// <summary>
        /// Updates the existing model in the database and return it
        /// </summary>
        /// <param name="id">The id of channel in DB</param>
        /// <param name="chanelModel">The model of channel that will be saved in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Returns updated channel model</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(ChannelModel), 200)]
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutChanel([FromRoute] string id, [FromBody] ChannelModel chanelModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            if (string.IsNullOrEmpty(chanelModel.Country) || string.IsNullOrEmpty(chanelModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _channelRepository.ReplaceAsync(chanelModel, model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));

            return Ok(chanelModel);
        }

        /// <summary>
        /// Delete the existing model in the database
        /// </summary>
        /// <param name="id">The id of channel in DB</param>
        /// <response code="400">Some fields in model are invalid or null</response>
        /// <response code="200">Channel was deleted successfully</response>
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

            await _channelRepository.DeleteByIdAsync(id);

            return Ok("Channel was deleted successfully");
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
            var path = Path.Combine(AppContext.BaseDirectory, "chanels.txt");

            if (!System.IO.File.Exists(path))
            {
                return NotFound($"File with channel not found. Expected path: {path}");
            }

            var data = await System.IO.File.ReadAllLinesAsync(path, CancellationToken.None);
            var listData = data.ToList();
            var listModels = listData.Select(JsonConvert.DeserializeObject<ChannelModel>).ToList();
            await _channelRepository.InsertRangeAsync(listModels);
            return Ok("DataBase was seeded successfully");
        }

    }
}
