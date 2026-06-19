using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestAprilEducation.API.Authorization
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            MinimumAgeRequirement requirement)
        {
            var birthdateClaim = context.User.FindFirst(ClaimTypes.DateOfBirth);

            if(birthdateClaim is null || !DateTime.TryParse(birthdateClaim.Value, out var birthdate))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var today = DateTime.Today;

            var age = today.Year - birthdate.Year;

            if (birthdate.Date > today.AddYears(-age))
            {
                age--;
            }   

            if (age < requirement.MinimumAge)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
