using ShopOnline.Domains;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.WebAdmin.BusinessLayers
{
    public interface IUserHandlers
    {
        Task<string> Authenticate(LoginRequest request);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}