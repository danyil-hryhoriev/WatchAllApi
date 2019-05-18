using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAll.Api.Interfaces.Managers;
using WatchAll.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WatchAll.Api.Enums;
using WatchAll.Api.Requests;

namespace WatchAll.Api.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the authorization
    /// </summary>
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IPasswordHasher<UserProfile> _passwordHasher;


        /// <summary>
        /// TokensController constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="passwordHasher"></param>
        public TokensController(IUserManager userManager, IAuthorizationManager authorizationManager, IPasswordHasher<UserProfile> passwordHasher)
        {
            _userManager = userManager;
            _authorizationManager = authorizationManager;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Login user via login and password
        /// </summary>
        /// <param name="loginModel">Model for authorization</param>
        /// <response code="404">Profile with give login not found</response>
        /// <response code="400">Invalid login model</response>
        /// <response code="200">Returns new token</response>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]UserLoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid model");
            }

            var profile = await _userManager.GetByLogin(loginModel.Username);

            if (profile != null)
            {
                var user = _authorizationManager.Authenticate(loginModel, profile);

                if (user != null)
                {
                    var result = GenerateToken(profile);
                    return Ok(new { User = user.Id, Token = result });
                }
                return Unauthorized();
            }

            return NotFound("Profile with give login not found");
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerRequest">Model of new user</param>
        /// <response code="401">Error occuring when logging</response>
        /// <response code="400">User already exist or invalid model</response>
        /// <response code="200">Returns new token</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 200)]
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

            var byEmail = await _userManager.GetByEmail(userModel.Email);

            if (byEmail != null)
            {
                return BadRequest();
            }

            var byLogin = await _userManager.GetByLogin(userModel.Login);

            if (byLogin != null)
            {
                return BadRequest();
            }

            var login = new UserLoginModel() { Username = userModel.Login, Password = userModel.Password };

            userModel.Password = _passwordHasher.HashPassword(userModel, userModel.Password);
            userModel.CreatedDate = DateTime.Now;
            userModel.Role = UserRole.User;

            await _userManager.InsertProfileAsync(userModel);
            
            var user = _authorizationManager.Authenticate(login, userModel);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = GenerateToken(userModel);
            return Ok(new {Token = result});
        }

        /// <summary>
        /// Seeds basic users
        /// </summary>
        /// <response code="200">Basic users seeded</response>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("seed"), HttpPost]
        [ProducesResponseType(typeof(string), 200)]
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
