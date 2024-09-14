using SistemaDeGestao.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoModel>> BuscarTodos();
        Task<ProdutoModel> BuscarPorId(int id);
        Task<ProdutoModel> Adicionar(ProdutoModel produto);
        Task<ProdutoModel> Atualizar(ProdutoModel produto, int id);
        Task<bool> Apagar(int id);
    }
}
