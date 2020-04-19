using ShopOnline.Domains;
using System.Threading.Tasks;

namespace ShopOnline.WebAdmin.ServiceLayers
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}