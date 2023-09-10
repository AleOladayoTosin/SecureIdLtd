using Microsoft.AspNetCore.Identity;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Domain;

namespace SecureId.AccessControl.API.Services
{
    public static class GetUserService
    {
        public static async Task<UserDto> GetUser(UserManager<AppUser> userManager, TokenService tokenService, AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = tokenService.CreateToken(user, roles.First()),
                Username = user.UserName
            };
        }
    }
}
