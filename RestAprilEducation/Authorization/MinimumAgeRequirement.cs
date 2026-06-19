using Microsoft.AspNetCore.Authorization;

namespace RestAprilEducation.API.Authorization
{
    public class MinimumAgeRequirement(int minimumAge) :  IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}