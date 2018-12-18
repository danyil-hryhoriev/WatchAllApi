using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface IAuthorizationManager
    {
        UserModel Authenticate(UserLoginModel loginModel, UserProfile profile);
    }
}
