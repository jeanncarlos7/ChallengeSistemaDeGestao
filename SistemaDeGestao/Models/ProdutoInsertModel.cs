using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeGestao.Models
{
    public class ProdutoInsertModel
    {

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Preco")]
        public decimal Preco { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }
    }
}
