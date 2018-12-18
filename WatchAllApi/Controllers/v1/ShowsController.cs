using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchAllApi.Enums;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowManager _showManager;
        public List<GenreModel> Genres { get; set; }
        public List<ChanelModel> Chanels { get; set; }
        Random rand = new Random();

        public ShowsController(IShowManager showManager)
        {
            _showManager = showManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShows()
        {
            var show = await _showManager.GetAllShows();
            return Ok(show);
        }

        [HttpGet]
        [Route("top100")]
        public async Task<IActionResult> GetTopShows()
        {
            var show = await _showManager.GetTopShows();
            List<ShowModel> top = new List<ShowModel>();

            for (int i = 0; i < 5; i++)
            {
                top.Add(show[rand.Next(100)]);
            }

            return Ok(top);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetShowById([FromRoute] string id)
        {
            var show = await _showManager.GetShowById(id);
            return Ok(show);
        }

        [HttpPost]
        public async Task<IActionResult> PostShow([FromBody] ShowModel showModel)
        {
            var res = await _showManager.InsertShow(showModel);

            return Ok(res);
        }

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


        [Route("seed")]
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            await _showManager.SeedDb();
            return Ok();
        }


    }
}
