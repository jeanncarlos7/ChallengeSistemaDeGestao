using SistemaDeGestao.Models;

namespace SistemaDeGestao.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BuscarTodas();
        Task<TarefaModel> BuscarPorId(int id);
        Task<TarefaModel> Adicionar(TarefaModel tarefa);
        Task<TarefaModel> Buscar(TarefaModel tarefa, int id);
        Task<bool> Apagar(int id);
        Task<TarefaModel> Atualizar(TarefaModel tarefaModel, int id);
    }
}
