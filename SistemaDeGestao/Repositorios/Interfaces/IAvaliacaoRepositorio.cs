using SistemaDeGestao.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Repositorios.Interfaces
{
    public interface IAvaliacaoRepositorio
    {
        Task<AvaliacaoModel> BuscarPorId(int id);
        Task<List<AvaliacaoModel>> BuscarTodasAvaliacao();
        Task<AvaliacaoModel> Adicionar(AvaliacaoModel avaliacao);
        Task<AvaliacaoModel> AtualizarAvaliacao(AvaliacaoModel avaliacao, int id);
        Task<bool> Apagar(int id);
    }
}
