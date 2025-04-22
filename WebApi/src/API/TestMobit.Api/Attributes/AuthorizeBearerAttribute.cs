using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TestMobit.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeBearerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public AuthorizeBearerAttribute()
            : base("Bearer")
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated || string.IsNullOrEmpty(Roles))
            {
                return;
            }
            var roles = Roles.Split(",");

            if (!roles.Any()) return;

            var isAuthorized = roles.ToList().Exists(role => user.IsInRole(role));
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
