using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpiceRack.Core.Application.DTOs.Account;
using SpiceRack.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Authenticate User",
            Description = "Authenticates a user and returns a JWT token if successful."
        )]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        [HttpPost("registerAdmin")]
        [Authorize(Roles = $"Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(   
            Summary = "Register Admin User",
            Description = "Registers a new user with the 'Admin' role. Only existing Admin users can call this endpoint."
        )]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
           
            return Ok(await _accountService.RegisterAdminUserAsync(request, origin));
        }

        [HttpPost("registerWeiter")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [SwaggerOperation(
            Summary = "Register Waiter User",
            Description = "Registers a new user with the 'Waiter' role."
        )]
        public async Task<IActionResult> RegisterWaitersAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
           
            return Ok(await _accountService.RegisterWaitersUserAsync(request, origin));
        }
    }
}
