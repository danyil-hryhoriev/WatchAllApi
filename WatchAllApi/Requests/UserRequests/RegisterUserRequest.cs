using WatchAllApi.Enums;
using WatchAllApi.Models;

namespace WatchAllApi.Requests
{
    /// <summary>
    /// Request for UserProfile
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Converting request to model
        /// </summary>
        /// <returns></returns>
        public UserProfile ToModel()
        {
            return new UserProfile
            {
                Login = Login,
                Password = Password,
                Email = Email,
                Role = UserRole.User,
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}
