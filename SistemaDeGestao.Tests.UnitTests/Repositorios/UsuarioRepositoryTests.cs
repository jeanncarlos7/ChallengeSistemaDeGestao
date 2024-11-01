using SistemaDeGestao.Data.Repositories;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Tests.UnitTests.Repositorios
{
    public class UsuarioRepositoryTests
    {
        private readonly UsuarioRepository _repository;

        public UsuarioRepositoryTests()
        {
            _repository = new UsuarioRepository();
        }

        [Fact]
        public void AddUsuario_ShouldAddUserSuccessfully()
        {
            // Arrange
            var user = new Usuario { Id = 1, Email = "test@example.com", Password = "password123" };

            // Act
            _repository.AddUsuario(user);
            var result = _repository.ObterUsuarioPorEmail("test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal("password123", result.Password);
        }

        [Fact]
        public void ObterUsuarioPorEmail_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = _repository.ObterUsuarioPorEmail("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateUsuario_ShouldUpdateUserPassword()
        {
            // Arrange
            var user = new Usuario { Id = 1, Email = "test@example.com", Password = "password123" };
            _repository.AddUsuario(user);

            var updatedUser = new Usuario { Id = 1, Email = "test@example.com", Password = "newpassword456" };

            // Act
            _repository.UpdateUsuario(updatedUser);
            var result = _repository.ObterUsuarioPorEmail("test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newpassword456", result.Password);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUserSuccessfully()
        {
            // Arrange
            var user = new Usuario { Id = 1, Email = "test@example.com", Password = "password123" };
            _repository.AddUsuario(user);

            // Act
            _repository.DeleteUser(1);
            var result = _repository.ObterUsuarioPorEmail("test@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteUser_ShouldDoNothing_WhenUserDoesNotExist()
        {
            // Arrange
            var initialCount = _repository.ObterUsuarioPorEmail("nonexistent@example.com");

            // Act
            _repository.DeleteUser(99);

            // Assert
            Assert.Null(initialCount); // Usuário não existia antes
        }
    }
}
