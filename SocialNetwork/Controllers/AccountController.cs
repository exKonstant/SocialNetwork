using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.Authentification;
using SocialNetwork.Authentification.Services;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthentificationService _authentificationService;

        public AccountController(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        /// <summary>
        /// Performs user log in.
        /// </summary>
        /// <param name="model">User log in model</param>
        /// <response code="200">If the log in action succeeded</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPost("LogIn")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LogIn([FromBody] UserLoginModel model)
        {
            var logInSuccess = await _authentificationService.LogIn(model.Email, model.Password);
            if (!logInSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Performs user log out.
        /// </summary>
        /// <response code="200">Always</response>
        [HttpPost("LogOut")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LogOut()
        {
            await _authentificationService.LogOut();
            return Ok();
        }

        /// <summary>
        /// Performs user registration.
        /// </summary>
        /// <param name="model">User registration model</param>
        /// <response code="200">If the registration action succeeded</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var identityErrors =
                await _authentificationService.Register(model.Email, model.Password, model.PasswordConfirm, model.Roles);
            if (identityErrors.Count() != 0)
            {
                return BadRequest(identityErrors);
            }
            return Ok();
        }
    }
}

