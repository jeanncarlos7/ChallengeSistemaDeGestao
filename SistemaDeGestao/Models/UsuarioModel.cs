using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaDeGestao.Models
{
    public class UsuarioModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<AvaliacaoModel> Avaliacoes { get; set; }
    }
}
