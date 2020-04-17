using Microsoft.AspNetCore.Identity;
using ShopOnline.Domains;
using System.Threading.Tasks;

namespace ShopOnline.Application.Systems.Users
{
    public interface IUsersService
    {
        Task<string> AuthenticateAsync(LoginRequest request);
        Task<string> RegisterAsync(RegisterRequest request);
    }
}