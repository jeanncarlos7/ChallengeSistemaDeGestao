using SistemaDeGestao.Models;

namespace SistemaDeGestao.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BuscarTodosUsuarios();
        Task<UsuarioModel> BuscarPorId(int id);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Buscar(UsuarioModel usuario, int id);
        Task<bool> Apagar(int id);
        Task<UsuarioModel> ObterPorId(int id);
        Task<UsuarioModel> Atualizar(UsuarioModel usuarioModel, int id);
    }
}
