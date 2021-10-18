using System;
using System.Collections.Generic;
using EmployeesMicroService.Helpers;
using EmployeesMicroService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeesMicroService.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController (ILogger<UsersController> logger, IUserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet ("/users/")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (IEnumerable<User>))]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll ()
        {
            try
            {
                var users = _service.GetAll ();
                return Ok (users);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex, "An error occurs while trying to get users list");
                throw;
            }
        }

        [HttpGet ("/users/{userId}")]
        public IActionResult GetUser (string userId)
        {
            return null;
        }

        [AllowAnonymous]
        [HttpPost ("/api/users/authenticate")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (User))]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult Authenticate ([FromBody] User user)
        {
            try
            {
                _logger.LogInformation ($"Trying to authenticate user {user.UserName}");
                var userData = _service.Authenticate (user.UserName, user.Password);

                if (userData == null)
                {
                    _logger.LogInformation ("Either username or password were incorrect!"); 
                    return BadRequest ("Username or password incorrect!");
                }

                _logger.LogInformation ($"Authentication successful! user: {user.UserName}");
                return Ok (userData);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex, $"An error occurs while trying to authenticate user {user.UserName}");
                throw;
            }
        }
    }
}
