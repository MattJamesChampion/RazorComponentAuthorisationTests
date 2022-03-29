using Microsoft.AspNetCore.Authorization;

namespace RazorComponentPolicyTests.Shared;

public static class Policy
{
    public const string UserPolicy = "UserPolicy";
    public const string AdminPolicy = "AdminPolicy";
}

public static class Requirements
{
    public class UserPolicy : IAuthorizationRequirement
    {
    }

    public class AdminPolicy : IAuthorizationRequirement
    {
    }
}

public static class Handlers
{
    public class UserHandler : AuthorizationHandler<Requirements.UserPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            Requirements.UserPolicy requirement)
        {
            //We're a user so this will always succeed
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class AdminHandler : AuthorizationHandler<Requirements.AdminPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            Requirements.AdminPolicy requirement)
        {
            //We're not an admin, so this will always fail
            context.Fail();
            return Task.CompletedTask;
        }
    }
}