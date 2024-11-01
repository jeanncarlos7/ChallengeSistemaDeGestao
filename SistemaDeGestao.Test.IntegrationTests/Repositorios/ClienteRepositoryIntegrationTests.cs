using Microsoft.EntityFrameworkCore;
using SistemaDeGestao.Data;
using SistemaDeGestao.Repositorios;
using SistemaDeGestao.Models;
using Xunit;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SistemaDeGestao.Tests.IntegrationTests.Repositorios
{
    public class ClienteRepositorioIntegrationTests
    {
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public ClienteRepositorioIntegrationTests()
        {
            // Conectar ao MongoDB (localmente para testes)
            _mongoClient = new MongoClient("mongodb://admin:password@localhost:27017"); // Ajuste a string de conexão conforme necessário
            _database = _mongoClient.GetDatabase("DB_SistemaTarefas");

            // Limpar a coleção de clientes antes de cada teste
            _database.DropCollection("Clientes");

            // Inicializar o repositório de clientes com a instância do MongoDB
            _clienteRepositorio = new ClienteRepositorio(_mongoClient);
        }

        [Fact]
        public async Task DeveAdicionarClienteComSucesso()
        {
            // Arrange
            var novoCliente = new ClienteModel
            {
                Nome = "Teste Cliente",
                Email = "teste@cliente.com"
            };

            // Act
            var clienteAdicionado = await _clienteRepositorio.Adicionar(novoCliente);

            // Assert
            Assert.NotNull(clienteAdicionado.Id); // O ID deve ser preenchido após a inserção
            Assert.Equal(novoCliente.Nome, clienteAdicionado.Nome);
            Assert.Equal(novoCliente.Email, clienteAdicionado.Email);

            // Verificar se o cliente foi realmente salvo no MongoDB
            var clienteNoBanco = await _clienteRepositorio.BuscarPorId(clienteAdicionado.Id);
            Assert.NotNull(clienteNoBanco);
            Assert.Equal(clienteAdicionado.Id, clienteNoBanco.Id);
            Assert.Equal("Teste Cliente", clienteNoBanco.Nome);
            Assert.Equal("teste@cliente.com", clienteNoBanco.Email);
        }
    }
}
