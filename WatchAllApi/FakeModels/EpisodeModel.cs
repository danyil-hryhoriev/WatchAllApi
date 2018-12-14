using System;

namespace ApiLoader.Models
{
    public class EpisodeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public string OrderNumber { get; set; }
        public DateTime AirDate { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
    }
}