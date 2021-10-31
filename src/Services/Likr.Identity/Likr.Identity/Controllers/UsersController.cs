﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4;
using Likr.Identity.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Likr.Identity.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;

        public UsersController(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManger.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.DisplayName,
                user.UserName
            });
        }
    }
}