using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeGestao.Models
{
    public class ProdutoModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Preco")]
        public decimal Preco { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }
    }
}
