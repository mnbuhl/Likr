﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Likr.Server
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register(string returnUrl)
        {
            return Redirect(_configuration.GetValue<string>("Authority")
                            + "/Identity/Account/Register?returnUrl=" + _configuration.GetValue<string>("ServiceUrl") +
                            returnUrl);
        }
    }
}