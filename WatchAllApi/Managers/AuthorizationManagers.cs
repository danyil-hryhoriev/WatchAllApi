using System;
using Microsoft.AspNetCore.Identity;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Models;

namespace WatchAllApi.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IPasswordHasher<UserProfile> _passwordHasher;

        public AuthorizationManager(IPasswordHasher<UserProfile> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public UserModel Authenticate(UserLoginModel loginModel, UserProfile profile)
        {
            UserModel user = null;

            var userNameMatches = string.Equals(loginModel.Username, profile.Login, StringComparison.InvariantCultureIgnoreCase);
            var passwordMatches = _passwordHasher.VerifyHashedPassword(profile, profile.Password, loginModel.Password) == PasswordVerificationResult.Success;

            if (userNameMatches && passwordMatches)
            {
                user = new UserModel(profile);
            }

            return user;
        }
    }
}
