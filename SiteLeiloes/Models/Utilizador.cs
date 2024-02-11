using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteLeiloes.Models
{
    public class Utilizador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get;  set; } = "";
        //public string? Nome { get; set; } = "";
        //public int? Idade { get; set; }
        //public string? Telefone { get; set; } = "";
        //public string? CC { get; set; } = "";
        //public string? Nif { get; set; } = "";
        //public float? Avaliacao_total { get; set; }
        //public int? Nr_avaliacoes { get; set; }

        public Utilizador()
        {
        }
        public Utilizador(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
