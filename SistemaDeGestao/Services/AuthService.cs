using System.Net.Http;
using System.Threading.Tasks;

namespace SistemaDeGestao.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://api.external-service.com/auth", new { email, password });
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                // Log exception and return false
                return false;
            }
        }
    }
}
