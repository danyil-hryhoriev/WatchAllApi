using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Constants;
using WatchAllApi.Interfaces.Managers;

namespace WatchAllApi.Controllers.v1
{
    [Authorize(Roles = RoleConstants.ADMIN)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.GetByIdAsync(userId);
            return Ok(user); 
        }
    }
}
