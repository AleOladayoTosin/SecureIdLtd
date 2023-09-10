using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.API.IntegrationEvent.Account;
using SecureId.AccessControl.API.Services;
using SecureId.AccessControl.Domain;

namespace SecureId.AccessControl.API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) => 
            HandleResult(await Mediator.Send(new AuthenticateEventHandler.Command {  loginRequest = loginDto }));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register) =>
            HandleResult(await Mediator.Send(new RegisterEventHandler.Command { registerRequest = register }));
    }
}
