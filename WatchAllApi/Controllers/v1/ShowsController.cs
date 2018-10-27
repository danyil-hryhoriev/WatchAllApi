using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAllApi.Interfaces;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        public ShowsController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        [Route("seeddb")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await System.IO.File.ReadAllLinesAsync(Path.Combine(AppContext.BaseDirectory, "data.txt"), CancellationToken.None);
            var listData = data.ToList();
            var listModels = listData.Select(JsonConvert.DeserializeObject<ShowModel>).ToList();
            await _showRepository.InsertRangeAsync(listModels);
            return Ok();
        }
        
    }
}
