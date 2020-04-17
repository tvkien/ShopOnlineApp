using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShopOnline.Domains;
using ShopOnline.Data.Entities;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace ShopOnline.Application.Systems.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly TokensJWT tokensJwt;

        public UsersService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokensJWT tokensJwt)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokensJwt = tokensJwt;
        }

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);

            if(user == null)
            {
                return null;
            }

            var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokensJwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(tokensJwt.Issuer,
                tokensJwt.Issuer,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                DoB = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return string.Empty;
            }

            return GetErrorsMessage(result);
        }

        private static string GetErrorsMessage(IdentityResult result)
        {
            var errors = new StringBuilder();

            foreach (var error in result.Errors)
            {
                errors.AppendLine(error.Description);
            }

            return errors.ToString();
        }
    }
}