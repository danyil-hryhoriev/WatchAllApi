using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface IAuthorizationManager
    {
        UserModel Authenticate(UserLoginModel loginModel, UserProfile profile);
    }
}
