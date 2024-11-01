using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using SistemaDeGestao.HttpObjects;
using SistemaDeGestao.Services.Interfaces;

namespace SistemaDeGestao.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public Task<object?> BuscarEnderecoPorCep(string v)
        {
            throw new NotImplementedException();
        }

        public async Task<ViaCepResponse> GetAddressAsync(string cep)
        {
            var response = await _retryPolicy.ExecuteAsync(() =>
                _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<ViaCepResponse>(content);
                return address;
            }
            return null;
        }
    }
}
