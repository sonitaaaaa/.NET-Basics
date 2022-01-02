using System.Security.Claims;
using BlogsDEMO.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            RoleId = httpContextAccessor.HttpContext?.User?.FindFirstValue("roleId");
            RoleName = httpContextAccessor.HttpContext?.User?.FindFirstValue("roleName");
        }

        public string UserId { get; }
        public string RoleId { get; }
        public string RoleName { get; }
    }
}