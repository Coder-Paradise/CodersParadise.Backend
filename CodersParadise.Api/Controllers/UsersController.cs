﻿using CodersParadise.Api.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodersParadise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = new List<UserResponse>()
                {
                    new UserResponse() { Email = "bababooey@gmail.com"},
                    new UserResponse() { Email = "amimaryan@yahoo.com"}
                };
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}