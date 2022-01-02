using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string Url { get; }

        public CustomAuthorizeAttribute()
        {
        }

        public CustomAuthorizeAttribute(string url)
        {
            Url = url;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var adminUuid = context.HttpContext?.User?.FindFirstValue("uuid");
            var endpointUrl = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;
            //var permissionRepository = context.HttpContext.RequestServices.GetRequiredService<IPermissionMasterRepositoryAsync>();
            //if (!await permissionRepository.CheckPermission(adminUuid, Url))
            //{
            //    throw new UnauthorizeException("Unauthorized");
            //}
            //if (!await permissionRepository.CheckPermissionByEndpoint(adminUuid, endpointUrl, method))
            //{
            //    throw new UnauthorizeException("Unauthorized");
            //}
        }
    }
}