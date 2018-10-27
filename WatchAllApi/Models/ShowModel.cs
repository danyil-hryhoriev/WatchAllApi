using System;
using System.Collections.Generic;

namespace WatchAllApi.Models
{
    public class ShowModel
    {
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public DateTime PremiereDate { get; set; }
        public string ShowUrl { get; set; }
        public string DayOfAir { get; set; }
        public string TimeOfAir { get; set; }
        public string Rating { get; set; }
        public ChanelModel Chanel { get; set; }
        public string ImdbId { get; set; }
        public string TheTvDbId { get; set; }
        public string ImageOriginal { get; set; }
        public string ImageMedium { get; set; }
        public string Description { get; set; }
        public List<SeasonModel> Seasons { get; set; }
        public List<AliasModel> Aliases { get; set; }
        public List<ActorModel> Actors { get; set; }
    }
}
