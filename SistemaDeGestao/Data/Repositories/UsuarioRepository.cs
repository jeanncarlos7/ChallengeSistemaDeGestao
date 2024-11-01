using SistemaDeGestao.Models;
using System.Collections.Generic;

namespace SistemaDeGestao.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _users = new();
        private SistemaTarefasDBContext context;

        public UsuarioRepository(SistemaTarefasDBContext context)
        {
            this.context = context;
        }

        public UsuarioRepository()
        {
        }

        public Usuario ObterUsuarioPorEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

        public void AddUsuario(Usuario user) => _users.Add(user);

        public void UpdateUsuario(Usuario user)
        {
            var existingUser = ObterUsuarioPorEmail(user.Email);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
            }
        }

        public void DeleteUser(int id) => _users.RemoveAll(u => u.Id == id);

        public void Adicionar(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
