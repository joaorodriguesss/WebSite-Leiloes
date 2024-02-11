using SiteLeiloes.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Licitacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int LeilaoId { get; set; }
    public int UtilizadorId { get; set; }
    public virtual Leilao Leilao { get; set; }
    public virtual Utilizador Utilizador { get; set; }
    public Licitacao(int leilaoId, int utilizadorId)
    {
        LeilaoId = leilaoId;
        UtilizadorId = utilizadorId;
    }
}

