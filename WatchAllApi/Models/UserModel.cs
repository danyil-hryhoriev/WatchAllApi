namespace WatchAllApi.Models
{
    public class UserModel
    {        
        public UserModel(UserProfile profile)
        {
            Id = profile.Id;
            Name = profile.Login;
            Role = profile.Role.ToString().ToLower();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

    }
}
