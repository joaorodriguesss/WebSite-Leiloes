using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteLeiloes.Models
{
    public class Leilao
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "O preço mínimo deve ser positivo.")]
        public float Preco_minimo { get; set; }

        public int ClienteId { get; set; }
        public int VendedorId { get;  set; }
        public int CarroId { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float Valor { get; set; }

        [Required]
        public DateTime Data_de_inicio { get; set; }

        [Required]
        public DateTime Data_de_fim { get; set; }

        [Url]
        public string ImagemUrl { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Utilizador Cliente { get; set; }

        [ForeignKey("VendedorId")]
        public virtual Utilizador Vendedor { get; set; }

        [ForeignKey("CarroId")]
        public virtual Carro Carro { get; set; }

        public Leilao() { }
    }
}
