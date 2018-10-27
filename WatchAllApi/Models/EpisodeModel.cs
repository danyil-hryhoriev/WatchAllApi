using System;

namespace WatchAllApi.Models
{
    public class EpisodeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int OrderNumber { get; set; }
        public DateTime AirDate { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
    }
}