using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SistemaDeGestao.Data;
using SistemaDeGestao.Data.Repositories;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Tests.IntegrationTests.Repositorios
{
    public class UsuarioRepositoryIntegrationTests
    {
        private const string DatabaseName = "TesteDb";
        private readonly Microsoft.EntityFrameworkCore.DbContextOptions<SistemaTarefasDBContext> _options;

        public UsuarioRepositoryIntegrationTests()
        {
            // Configura o DbContext para usar um banco de dados em memória com o nome especificado
            _options = new DbContextOptionsBuilder<SistemaTarefasDBContext>()
                .UseInMemoryDatabase(databaseName: DatabaseName)
                .Options;
        }

        [Fact]
        public void DeveAdicionarUsuarioNoBancoDiretamente()
        {
            using (var context = new SistemaTarefasDBContext(_options))
            {
                var usuario = new UsuarioModel { Nome = "João", Email = "teste@teste.com" };
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                var usuarioNoBanco = context.Usuarios.Find(usuario.Id);
                Assert.Equal("João", usuarioNoBanco.Nome);
            }
        }
    }
}

