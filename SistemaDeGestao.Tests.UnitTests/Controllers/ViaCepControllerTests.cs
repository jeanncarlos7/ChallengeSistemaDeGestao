using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaDeGestao.Controllers;
using SistemaDeGestao.HttpObjects;
using SistemaDeGestao.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Tests.UnitTests.Controllers
{
    public class ViaCepControllerTests
    {
        private readonly ViaCepController _controller;
        private readonly Mock<IViaCepService> _mockViaCepService;

        public ViaCepControllerTests()
        {
            _mockViaCepService = new Mock<IViaCepService>();
            _controller = new ViaCepController(_mockViaCepService.Object);
        }

        [Fact]
        public async Task GetAddressByCep_ReturnsOkResult_WhenCepIsValid()
        {
            // Arrange
            var validCep = "01001000";
            var mockAddress = new ViaCepResponse
            {
                Cep = validCep,
                Logradouro = "Praça da Sé",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP"
            };

            _mockViaCepService.Setup(service => service.GetAddressAsync(validCep))
                              .ReturnsAsync(mockAddress);

            // Act
            var result = await _controller.GetAddressByCep(validCep);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var address = Assert.IsType<ViaCepResponse>(okResult.Value);
            Assert.Equal(validCep, address.Cep);
            Assert.Equal("Praça da Sé", address.Logradouro);
            Assert.Equal("Sé", address.Bairro);
            Assert.Equal("São Paulo", address.Localidade);
            Assert.Equal("SP", address.Uf);
        }

        [Fact]
        public async Task GetAddressByCep_ReturnsBadRequest_WhenCepIsNullOrWhiteSpace()
        {
            // Arrange
            var invalidCep = ""; // Empty or whitespace is invalid

            // Act
            var result = await _controller.GetAddressByCep(invalidCep);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("CEP não pode ser nulo ou vazio.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAddressByCep_ReturnsNotFound_WhenCepNotFound()
        {
            // Arrange
            var validCep = "01001000";

            _mockViaCepService.Setup(service => service.GetAddressAsync(validCep))
                              .ReturnsAsync((ViaCepResponse)null); // Simulando que o CEP não foi encontrado

            // Act
            var result = await _controller.GetAddressByCep(validCep);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Endereço não encontrado para o CEP fornecido.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAddressByCep_ReturnsInternalServerError_WhenHttpRequestExceptionOccurs()
        {
            // Arrange
            var validCep = "01001000";

            _mockViaCepService.Setup(service => service.GetAddressAsync(validCep))
                              .ThrowsAsync(new HttpRequestException("Erro na API externa"));

            // Act
            var result = await _controller.GetAddressByCep(validCep);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Erro ao consumir a API ViaCep: Erro na API externa", internalServerErrorResult.Value);
        }
    }
}
