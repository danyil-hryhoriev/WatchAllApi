namespace WatchAll.Api.Models
{
    /// <summary>
    /// Model of use
    /// </summary>
    public class UserModel
    {        
        /// <summary>
        /// Constructor for model of user
        /// </summary>
        /// <param name="profile"></param>
        public UserModel(UserProfile profile)
        {
            Id = profile.Id;
            Name = profile.Login;
            Role = profile.Role.ToString().ToLower();
        }

        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role of user in application
        /// </summary>
        public string Role { get; set; }

    }
}
