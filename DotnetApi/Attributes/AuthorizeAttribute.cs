using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using DotnetApi.CoreLib.Users.Entities;
using Microsoft.AspNetCore.Authorization;
using DotnetApi.CoreLib.QuanLyUser.DTO;

namespace DotnetApi.Attributes
{
    // [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] AllowedRole { get; set; }
        public CustomAuthorizeAttribute(params string[] roles)
        {
            AllowedRole = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserGet)context.HttpContext.Items["User"];
            if (user == null || !AllowedRole.Contains(user.RoleId))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}