using System;
using System.Collections.Generic;

namespace ApiLoader.Models
{
    public class SeasonModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string EpisodeQty { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}