using SistemaDeGestao.Services;
using System.Net;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaDeGestao.Tests.SystemTests.Services
{
    public class ViaCepServiceSystemTests
    {
        [Fact]
        public async Task DeveRetornarDadosDoViaCep()
        {
            // Configura o mock do HttpClient
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'cep':'12345-678','logradouro':'Rua Teste'}"),
            });

            var client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://viacep.com.br")
            };

            var service = new ViaCepService(client);
            var endereco = await service.GetAddressAsync("12345-789");

            Assert.NotNull(endereco);
            Assert.Equal("12345-678", endereco.Cep);
            Assert.Equal("Rua Teste", endereco.Logradouro);
        }

        //private class Mock<T>
        //{
        //    public Mock()
        //    {
        //    }

        //    public HttpMessageHandler Object { get; internal set; }

        //    internal object Protected()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
