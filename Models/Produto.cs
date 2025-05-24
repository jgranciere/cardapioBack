using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprendendoAPI.Models
{
    [Table("produtos")]
    public class Produto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        [Column("imagemurl")]
        public string? ImagemUrl { get; set; }

        [Column("categoria")]
        public string? Categoria { get; set; }

        public Produto(string nome, decimal preco, string descricao, string imagemUrl)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            ImagemUrl = imagemUrl;
        }

        
    }
}
