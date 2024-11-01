using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using SistemaDeGestao.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using SistemaDeGestao.Data;

namespace SistemaDeGestao.Tests.IntegrationTests.Controllers
{
    public class ClienteControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClienteControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder
                (builder =>
            {
                builder.ConfigureServices((IServiceCollection services) =>
                {
                    //services.RemoveAll<IMeuServico>();
                    //services.AddScoped<IMeuServico, MeuServicoMock>();

                    services.RemoveAll<DbContextOptions<SistemaTarefasDBContext>>();
                    services.AddDbContext<SistemaTarefasDBContext>(options =>
                        options.UseInMemoryDatabase("TestDatabase"));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task DeveRetornarListaDeClientes()
        {
            var response = await _client.GetAsync("/api/Cliente");

            Assert.True(response.IsSuccessStatusCode);

            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();
            //var clientes = JsonSerializer.Deserialize<List<ClienteModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //Assert.NotNull(clientes);
            //Assert.NotEmpty(clientes);
        }
    }
}
