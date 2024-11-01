using SistemaDeGestao.Models;

namespace SistemaDeGestao.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuarioPorEmail(string email);
        void AddUsuario(Usuario user);
        void UpdateUsuario(Usuario user);
        void DeleteUser(int id);
    }
}
