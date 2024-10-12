using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using SistemaDeGestao;

namespace SistemaDeGestao.Tests.SystemTests
{
    public class EndToEndTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EndToEndTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var client = _factory.CreateClient();
            var content = new StringContent("{\"email\":\"email@example.com\", \"password\":\"password123\"}", System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/auth/login", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
