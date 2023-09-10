using Microsoft.AspNetCore.Http;
using SecureId.AccessControl.Application.Interfaces;
using System.Security.Claims;

namespace SecureId.AccessControl.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
                _contextAccessor = httpContextAccessor;
        }
        public string GetUsername()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
