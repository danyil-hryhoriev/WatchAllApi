using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChanelsController : ControllerBase
    {
        private readonly IChanelRepository _chanelRepository;
        public ChanelsController(IChanelRepository chanelRepository)
        {
            _chanelRepository = chanelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChanels()
        {
            var chanels = await _chanelRepository.SelectAllAsync();
            return Ok(chanels);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetChanelById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }
            var chanels = await _chanelRepository.FindAsync(id);
            return Ok(chanels);
        }

        [HttpPost]
        public async Task<IActionResult> PostChanel([FromBody] ChanelModel chanelModel)
        {
            if (string.IsNullOrEmpty(chanelModel.Country) || string.IsNullOrEmpty(chanelModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _chanelRepository.InsertAsync(chanelModel);

            return Ok(chanelModel);
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> PostChanel([FromRoute] string id, [FromBody] ChanelModel chanelModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            if (string.IsNullOrEmpty(chanelModel.Country) || string.IsNullOrEmpty(chanelModel.Name))
            {
                return BadRequest("Model is invalid");
            }

            await _chanelRepository.ReplaceAsync(chanelModel, model => string.Equals(model.Id, id, StringComparison.OrdinalIgnoreCase));

            return Ok(chanelModel);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteChanelById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is empty");
            }

            var chanels = await _chanelRepository.DeleteByIdAsync(id);

            return Ok(chanels);
        }

        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var data = await System.IO.File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "DataInit", "chanels.txt"), CancellationToken.None);
            var listData = data.ToList();
            var listModels = listData.Select(JsonConvert.DeserializeObject<ChanelModel>).ToList();
            await _chanelRepository.InsertRangeAsync(listModels);
            return Ok();
        }

    }
}
