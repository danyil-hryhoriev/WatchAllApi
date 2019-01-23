using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Models;
using WatchAllApi.Requests.UserRequests;
using WatchAllApi.Responses.UserResponses;

namespace WatchAllApi.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the users
    /// </summary>
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// UserController constructor
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Returns info about user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Mock</response>
        [ProducesResponseType(typeof(UserProfileRequest), 200)]
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.GetById(userId);
            return Ok(UserProfileRequest.Create(user)); 
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Mock</response>
        [ProducesResponseType(typeof(UserProfileRequest), 200)]
        [HttpPut]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserProfileRequest profileRequest)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.GetById(userId);

            var toInsert = profileRequest.MergeToModel(user);

            await _userManager.UpdateProfileAsync(toInsert);

            return Ok(UserProfileRequest.Create(toInsert));
        }
    }
}
