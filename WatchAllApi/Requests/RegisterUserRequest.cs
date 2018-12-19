using WatchAllApi.Enums;
using WatchAllApi.Models;

namespace WatchAllApi.Requests
{
    public class RegisterUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserProfile ToModel()
        {
            return new UserProfile()
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
