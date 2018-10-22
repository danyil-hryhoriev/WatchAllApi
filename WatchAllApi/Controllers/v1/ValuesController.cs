using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Interfaces;
using WatchAllApi.Models;

namespace WatchAllApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        public ValuesController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }
       
        [HttpGet]
        public ActionResult<string> Get()
        {
            _showRepository.InsertAsync(new ShowModel()
            {
                Name = "Castle",
                Description = "Lorem ipsum",
                Mark = 8.0,
                Seasons = new List<SeasonModel>()
                {
                    new SeasonModel()
                    {
                        SeasonId = 1,
                        SeriesModels = new List<SeriesModel>()
                        {
                            new SeriesModel()
                            {
                                Name = "First",
                                RealeseDate = DateTime.Today,
                                SeriesId = 1
                            }
                        }
                    }
                },
                State = SerialStateEnum.OnAir
            });
            return Ok("asf");
        }
        
    }
}
