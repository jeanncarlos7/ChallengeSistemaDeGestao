using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;

namespace SistemaDeGestao.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public AuthService(HttpClient client)
        {
            _client = client;

            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); 
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _client.PostAsJsonAsync("https://api.external-service.com/auth", new { email, password }));

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {

                return false;
            }
        }
    }
}
