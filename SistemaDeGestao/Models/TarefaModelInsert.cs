using SistemaDeGestao.Enums;

namespace SistemaDeGestao.Models
{
    public class TarefaModelInsert
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
    }
}
