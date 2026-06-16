using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAprilEducation.Application.Users.Create;
using RestAprilEducation.Application.Users.Login;
using RestAprilEducation.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RestAprilEducation.Application
{
    public class UserApplication(UserManager<AppUser> userManager, IConfiguration configuration)
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

        public async Task<ApplicationResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                return ApplicationResult<LoginResponse>.Failure("Email veya şifre hatalı", HttpStatusCode.Unauthorized);
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return ApplicationResult<LoginResponse>.Failure("Email veya şifre hatalı", HttpStatusCode.Unauthorized);
            }

            var token = GenerateJwtToken(user);
            return ApplicationResult<LoginResponse>.Success(token, HttpStatusCode.OK);
        }

        private LoginResponse GenerateJwtToken(AppUser user)
        {
            var jwtSection = configuration.GetSection("Jwt");
            var secretKey = jwtSection["SecretKey"]!;
            var issuer = jwtSection["Issuer"]!;
            if (!int.TryParse(jwtSection["ExpirationInMinutes"], out var expirationInMinutes))
            {
                // choose either to throw a clear exception or fall back to a default
                throw new InvalidOperationException("Configuration invalid or missing: Jwt:ExpirationInMinutes");
                // or: expirationInMinutes = 60;
            }


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if(!string.IsNullOrEmpty(user.City))
            {
                claims.Add(new ("city", user.City!));
            }

            var expiration = DateTime.UtcNow.AddMinutes(expirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            ); 
             
            return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), expiration);

        }
    }
}
