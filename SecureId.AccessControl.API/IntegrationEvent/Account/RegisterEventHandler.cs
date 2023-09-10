
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.API.Services;
using SecureId.AccessControl.Domain;

namespace SecureId.AccessControl.API.IntegrationEvent.Account
{
    public class RegisterEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public RegisterDto  registerRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseMessage>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly TokenService _tokenService;
            public Handler(UserManager<AppUser> userManager, TokenService tokenService)
            {
                _userManager = userManager;
                _tokenService = tokenService;
            }
            public async Task<ResponseMessage> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName == request.registerRequest.Username))
                    return new ResponseMessage { Message = "Username is already taken", Status = false };

                if (await _userManager.Users.AnyAsync(x => x.Email == request.registerRequest.Email))
                    return new ResponseMessage { Message = "Email is already taken", Status = false };

                var user = new AppUser
                {
                    DisplayName = request.registerRequest.DisplayName,
                    Email = request.registerRequest.Email,
                    UserName = request.registerRequest.Username,
                };

                var result = await _userManager.CreateAsync(user, request.registerRequest.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.registerRequest.Role);
                    return new ResponseMessage
                    {
                        Message = "User Created Successfully,Please Login",
                        Status = true,
                        Data = GetUserService.GetUser(_userManager, _tokenService, user)
                    };

                }
                return new ResponseMessage {Status = false, Data = result.Errors };

            }

        }
    }
}
