using Microsoft.AspNetCore.Authorization;

namespace TestMobit.Api.Requirements
{
    public class RoleRequirementHandler :
        AuthorizationHandler<RoleRequirement, string>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleRequirement requirement,
            string resource)
        {
            if (context.User.IsInRole(resource))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
