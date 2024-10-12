using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using SistemaDeGestao.Services;

namespace SistemaDeGestao.Tests.IntegrationTests
{
    public class AuthIntegrationTests
    {
        private readonly AuthService _authService;

        public AuthIntegrationTests()
        {
            // Aqui usamos um HttpClient real para integração com um serviço externo
            var httpClient = new HttpClient { BaseAddress = new System.Uri("https://api.external-service.com") };
            _authService = new AuthService(httpClient);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ShouldReturnTrue_WhenServiceRespondsSuccessfully()
        {
            // Act
            var result = await _authService.AuthenticateUserAsync("email@example.com", "password123");

            // Assert
            Assert.True(result);
        }
    }
}
