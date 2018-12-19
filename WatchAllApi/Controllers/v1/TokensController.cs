using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WatchAllApi.Enums;
using WatchAllApi.Requests;

namespace WatchAllApi.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IPasswordHasher<UserProfile> _passwordHasher;

        public TokensController(IUserManager userManager, IAuthorizationManager authorizationManager, IPasswordHasher<UserProfile> passwordHasher)
        {
            _userManager = userManager;
            _authorizationManager = authorizationManager;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]UserLoginModel loginModel)
        {
            UserProfile profile = null;

            if (!string.IsNullOrEmpty(loginModel.Username))
            {
                profile = await _userManager.GetByLoginAsync(loginModel.Username);
            }

            if (profile != null)
            {
                var user = _authorizationManager.Authenticate(loginModel, profile);

                if (user != null)
                {
                    var result = GenerateToken(profile);
                    return Ok(result);
                }
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserRequest registerRequest)
        {
            var userModel = registerRequest.ToModel();

            if (userModel == null)
            {
                return BadRequest();
            }

            var byEmail = await _userManager.GetByEmailAsync(userModel.Email);

            if (byEmail != null)
            {
                return BadRequest();
            }

            var byLogin = await _userManager.GetByLoginAsync(userModel.Login);

            if (byLogin != null)
            {
                return BadRequest();
            }

            userModel.Password = _passwordHasher.HashPassword(userModel, userModel.Password);
            userModel.CreatedDate = DateTime.Now;
            userModel.Role = UserRole.User;

            await _userManager.InsertProfileAsync(userModel);

            var login = new UserLoginModel() { Username = userModel.Login, Password = userModel.Password };

            var user = _authorizationManager.Authenticate(login, userModel);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = GenerateToken(userModel);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("seed"), HttpPost]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> SeedAdmin()
        {
            var profiles = new[]
            {
                new UserProfile{
                    Login = "mozgokluy",
                    CreatedDate = DateTime.Now,
                    Email = "mozgopluy@gmail.com",
                    FirstName = "Danyil",
                    LastName = "Hryhoriev",
                    Password = "12345678",
                    Role = UserRole.Admin
                },
                new UserProfile
                {
                    Login = "user1",
                    CreatedDate = DateTime.Now,
                    Email = "qwerty@gmail.com",
                    FirstName = "Danyil",
                    LastName = "Hryhoriev",
                    Password = "12345678",
                    Role = UserRole.User
                }
            };

            foreach (var user in profiles)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                await _userManager.InsertProfileAsync(user);
            }

            return Ok(profiles);
        }

        private string GenerateToken(UserProfile profile)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1235467887654321qwerty"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, profile.Id),
                new Claim(ClaimTypes.Role, profile.Role.ToString().ToLower())
            };


            var token = new JwtSecurityToken(
                "https://identity.watch-all.com/",
                "Audience",
                claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
