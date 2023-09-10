
using MediatR;
using Microsoft.AspNetCore.Identity;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.API.Services;
using SecureId.AccessControl.Domain;

namespace SecureId.AccessControl.API.IntegrationEvent.Account
{
    public class AuthenticateEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public LoginDto  loginRequest { get; set; }
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
                var user = await _userManager.FindByEmailAsync(request.loginRequest.Email);

                if (user == null)
                    return new ResponseMessage
                    {
                        Message = "User detail is invalid, please try again with correct details",
                        Status = false
                    };


                var result = await _userManager.CheckPasswordAsync(user, request.loginRequest.Password);

                if (result)
                {
                    return new ResponseMessage
                    {
                        Message = "Login Successful",
                        Status = true,
                        Data = GetUserService.GetUser(_userManager, _tokenService, user)
                    };
                }

                return new ResponseMessage { Message = "Something went wrong", Status = false };

            }

        }
    }
}
