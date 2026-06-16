using Microsoft.AspNetCore.Identity;
using RestAprilEducation.Application.Users.Create;
using RestAprilEducation.Domain;
using System.Net;

namespace RestAprilEducation.Application
{
    public class UserApplication(UserManager<AppUser> userManager)
    {
        public async Task<ApplicationResult<CreateUserResponse>> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var user = new AppUser
            {
                UserName = createUserRequest.Username,
                Email = createUserRequest.Email,
            };

            var result = await userManager.CreateAsync(user, createUserRequest.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                return ApplicationResult<CreateUserResponse>.Failure(errors, HttpStatusCode.BadRequest);
            }

            return ApplicationResult<CreateUserResponse>.Success(
                new CreateUserResponse(user.Id),
                HttpStatusCode.Created);
        }

    }
}
