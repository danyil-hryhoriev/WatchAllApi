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
    public class ChanelsController : ControllerBase
    {
        private readonly IChanelRepository _chanelRepository;
        public ChanelsController(IChanelRepository chanelRepository)
        {
            _chanelRepository = chanelRepository;
        }

        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            var data = await System.IO.File.ReadAllLinesAsync(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "DataInit", "chanels.txt"), CancellationToken.None);
            var listData = data.ToList();
            var listModels = listData.Select(JsonConvert.DeserializeObject<ChanelModel>).ToList();
            await _chanelRepository.InsertRangeAsync(listModels);
            return Ok();
        }

    }
}
