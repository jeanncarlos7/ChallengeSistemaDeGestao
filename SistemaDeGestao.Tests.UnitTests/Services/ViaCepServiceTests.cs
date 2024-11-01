using Moq;
using Moq.Protected;
using SistemaDeGestao.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Tests.UnitTests.Services
{
    public class ViaCepServiceTests
    {
        [Fact]
        public async Task GetAddressAsync_ReturnsValidResponse_WhenCepIsValid()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"cep\":\"01001-000\",\"logradouro\":\"Praça da Sé\",\"bairro\":\"Sé\",\"localidade\":\"São Paulo\",\"uf\":\"SP\"}")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var viaCepService = new ViaCepService(httpClient);

            // Act
            var result = await viaCepService.GetAddressAsync("01001000");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("01001-000", result.Cep);
            Assert.Equal("Praça da Sé", result.Logradouro);
            Assert.Equal("Sé", result.Bairro);
            Assert.Equal("São Paulo", result.Localidade);
            Assert.Equal("SP", result.Uf);
        }

        [Fact]
        public async Task GetAddressAsync_ThrowsException_WhenApiResponseIsBadRequest()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var viaCepService = new ViaCepService(httpClient);

            var obj = await viaCepService.GetAddressAsync("12345");

            // Act & Assert
            Assert.Null(obj);
        }
    }
}
