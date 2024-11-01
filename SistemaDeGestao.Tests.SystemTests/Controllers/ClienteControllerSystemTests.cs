using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestao.Models;
using System.Text.Json;
using SistemaDeGestao.Data;
using MongoDB.Bson;

namespace SistemaDeGestao.Tests.SystemTests.Controllers
{
    public class ClienteControllerSystemTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClienteControllerSystemTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<DbContextOptions<SistemaTarefasDBContext>>();
                    services.AddDbContext<SistemaTarefasDBContext>(options =>
                        options.UseInMemoryDatabase("TestDatabase"));

                    // Popule o banco com dados de exemplo
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<SistemaTarefasDBContext>();
                    dbContext.Clientes.Add(new ClienteModel { Id = ObjectId.GenerateNewId().ToString(), Nome = "Cliente Teste", Email = "teste@teste.com" });
                    dbContext.SaveChanges();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task DeveRetornarListaDeClientes()
        {
            var response = await _client.GetAsync("/api/Cliente");
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
