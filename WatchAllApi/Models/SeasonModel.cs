using System;
using System.Collections.Generic;

namespace WatchAllApi.Models
{
    public class SeasonModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int EpisodeQty { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}