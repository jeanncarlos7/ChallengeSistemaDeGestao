using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SistemaDeGestao.Services;

namespace SistemaDeGestao.Tests.UnitTests
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task AuthenticateUserAsync_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>(); // Mock do HttpClient, necessário para simular as respostas da API externa
            var authService = new AuthService(mockHttpClient.Object);

            // Act
            var result = await authService.AuthenticateUserAsync("email@example.com", "password123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ShouldReturnFalse_WhenCredentialsAreInvalid()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            var authService = new AuthService(mockHttpClient.Object);

            // Act
            var result = await authService.AuthenticateUserAsync("invalid@example.com", "wrongpassword");

            // Assert
            Assert.False(result);
        }
    }
}
