using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteLeiloes.Models
{
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; internal set; } = "";
        public string Password { get; internal set; } = "";
        public Administrador(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}