using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteLeiloes.Models
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Data_fim { get; set; }
        public float Preco { get; set; }
        public int VendedorId { get; set; }
        public int ClienteId { get; set; }
        public int CarroId { get; set; }
        public virtual Utilizador Vendedor { get; set; }
        public virtual Utilizador Cliente { get; set; }
        public virtual Carro Carro { get; set; }

        public Venda(DateTime data_fim, float preco, int vendedorId, int clienteId, int carroId)
        {
         
            Data_fim = data_fim;
            Preco = preco;
            VendedorId = vendedorId;
            ClienteId = clienteId;
            CarroId = carroId;
        }
    }
}