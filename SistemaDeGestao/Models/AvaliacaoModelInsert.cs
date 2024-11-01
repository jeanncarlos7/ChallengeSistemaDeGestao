using SistemaDeGestao.Enums;

namespace SistemaDeGestao.Models
{
    public class AvaliacaoModelInsert
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
        public string Email { get; set; }
    }
}
