using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchAllApi.Constants;
using WatchAllApi.Interfaces.Managers;

namespace WatchAllApi.Controllers.v1
{
    [Authorize(Roles = RoleConstants.ADMIN)]
    [Route("api/v1/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId); 
        }
    }
}
