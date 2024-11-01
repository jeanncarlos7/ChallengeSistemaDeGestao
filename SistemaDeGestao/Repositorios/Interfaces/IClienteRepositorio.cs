
using SistemaDeGestao.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Repositorios.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteModel>> BuscarTodosClientes();
        Task<ClienteModel> BuscarPorId(string id);
        Task<ClienteModel> Adicionar(ClienteModel cliente);
        Task<bool> Atualizar(string id, ClienteModel cliente);
        Task<bool> Apagar(string id);
    }
}
