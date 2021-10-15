using System;
using System.Threading.Tasks;
using IdentityServer4;
using Likr.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Likr.Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;

        public UsersController(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger;
        }
        
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userManger.FindByNameAsync(username);

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.DisplayName,
                user.Image
            });
        }
    }
}