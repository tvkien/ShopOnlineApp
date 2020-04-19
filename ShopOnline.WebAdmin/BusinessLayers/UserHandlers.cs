using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopOnline.Domains;
using ShopOnline.WebAdmin.ServiceLayers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.WebAdmin.BusinessLayers
{
    public class UserHandlers : IUserHandlers
    {
        private readonly IUserApiClient userApiClient;
        private readonly TokensJWT tokensJWT;
        private readonly ISecurityTokenValidator securityTokenValidator;

        public UserHandlers(
            IUserApiClient userApiClient,
            TokensJWT tokensJWT,
            ISecurityTokenValidator securityTokenValidator)
        {
            this.userApiClient = userApiClient;
            this.tokensJWT = tokensJWT;
            this.securityTokenValidator = securityTokenValidator;
        }

        public async Task<string> Authenticate(LoginRequest request) 
            => await userApiClient.Authenticate(request);

        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidAudience = tokensJWT.Issuer,
                ValidIssuer = tokensJWT.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokensJWT.Key))
            };

            var claims = securityTokenValidator.ValidateToken(
                jwtToken,
                validationParameters,
                out SecurityToken validatedToken);

            return claims;
        }
    }
}