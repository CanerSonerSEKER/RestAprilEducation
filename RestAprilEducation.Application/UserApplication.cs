using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAprilEducation.Application.Users.Create;
using RestAprilEducation.Application.Users.Login;
using RestAprilEducation.Domain;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RestAprilEducation.Application
{
    public class UserApplication(
        UserManager<AppUser> userManager, 
        RoleManager<AppRole> roleManager, 
        IConfiguration configuration)
    {
        public async Task<ApplicationResult<CreateUserResponse>> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var user = new AppUser
            {
                UserName = createUserRequest.Username,
                Email = createUserRequest.Email,
                BirthDate = createUserRequest.BirthDate
            };

            var result = await userManager.CreateAsync(user, createUserRequest.Password);


            var persistedUser = await userManager.FindByNameAsync(user.UserName);

            if (persistedUser is null)
            {
                return ApplicationResult<CreateUserResponse>.Failure("Kullanıcı oluşturulamadı", HttpStatusCode.NotFound);
            }

            // Claim based(policy based) authorization için claim ekleyelim ve kullanıcıya atayalım
            await userManager.AddClaimAsync(user, 
                new Claim(JwtRegisteredClaimNames.Birthdate, createUserRequest.BirthDate.ToString(CultureInfo.InvariantCulture)));


            // Role based authorization için role ekleyelim ve kullanıcıya atayalım
            await roleManager.CreateAsync(new AppRole()
            {
                Name = "editor"
            });

            await userManager.AddToRoleAsync(user, "editor");

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

            var token = await GenerateJwtToken(user);
            return ApplicationResult<LoginResponse>.Success(token, HttpStatusCode.OK);
        }

        private async Task<LoginResponse> GenerateJwtToken(AppUser user)
        {
            var jwtSection = configuration.GetSection("Jwt");
            var secretKey = jwtSection["SecretKey"]!;
            var audience = jwtSection["Audience"]!;
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

                new(ClaimTypes.Name, user.UserName.ToString())
            };


            if(!string.IsNullOrEmpty(user.City))
            {
                claims.Add(new("city", user.City!));
            }

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var userClaims = await userManager.GetClaimsAsync(user);

            foreach(var userClaim in userClaims)
            {
                claims.Add(userClaim);
            }

            var expiration = DateTime.UtcNow.AddMinutes(expirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            ); 
             
            return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), expiration);

        }
    }
}
