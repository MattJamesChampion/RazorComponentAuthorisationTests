using Microsoft.AspNetCore.Authorization;

namespace RazorComponentPolicyTests.Shared;

public static class Policy
{
    public const string SuccessPolicy = "SuccessPolicy";
    public const string FailurePolicy = "FailurePolicy";
}

public static class Requirements
{
    public class SuccessRequirement : IAuthorizationRequirement
    {
    }

    public class FailureRequirement : IAuthorizationRequirement
    {
    }
}

public static class Handlers
{
    public class SuccessHandler : AuthorizationHandler<Requirements.SuccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            Requirements.SuccessRequirement requirement)
        {
            //We always want this to succeed
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class FailureHandler : AuthorizationHandler<Requirements.FailureRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            Requirements.FailureRequirement requirement)
        {
            //This should always fail
            context.Fail();
            return Task.CompletedTask;
        }
    }
}