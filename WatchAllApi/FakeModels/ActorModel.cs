using System;

namespace ApiLoader.Models
{
    public class ActorModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime DeathDay { get; set; }
        public GenderEnum Gender { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string CharacterName { get; set; }
    }
}