namespace ApiLoader.Models
{
    public class CrewModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Birthday { get; set; }
        public object Deathday { get; set; }
        public GenderEnum Gender { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
    }
}
