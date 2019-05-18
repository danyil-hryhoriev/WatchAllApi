using System;
using Microsoft.AspNetCore.Identity;
using WatchAll.Api.Interfaces.Managers;
using WatchAll.Api.Models;

namespace WatchAll.Api.Managers
{
    /// <summary>
    /// Authorization manager
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IPasswordHasher<UserProfile> _passwordHasher;

        /// <summary>
        /// Constructor of Authorization manager
        /// </summary>
        /// <param name="passwordHasher"></param>
        public AuthorizationManager(IPasswordHasher<UserProfile> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Authorizing existing user
        /// </summary>
        /// <param name="loginModel">Model for authorization</param>
        /// <param name="profile">Existing user profile</param>
        /// <returns></returns>
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
