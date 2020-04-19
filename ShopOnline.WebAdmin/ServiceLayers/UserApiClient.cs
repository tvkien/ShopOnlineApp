using ShopOnline.Domains;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.WebAdmin.ServiceLayers
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var client = httpClientFactory.CreateClient(HttpClientName.BackendApi);
            var result = await client.PostAsJsonAsync("api/users/authenticate", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }

            var a = await result.Content.ReadAsStringAsync();

            return a;
        }
    }
}