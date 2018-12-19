using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Constants;
using WatchAllApi.Interfaces.Managers;

namespace WatchAllApi.Controllers.v1
{
    /// <summary>
    /// The controller allows managing the users
    /// </summary>
    [Authorize(Roles = RoleConstants.ADMIN)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// UsersController constructor
        /// </summary>
        /// <param name="userManager"></param>
        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Mock
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Mock</response>
        [ProducesResponseType(typeof(string), 200)]
        [HttpGet]
        public IActionResult GetInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.GetById(userId);
            return Ok(user); 
        }
    }
}
