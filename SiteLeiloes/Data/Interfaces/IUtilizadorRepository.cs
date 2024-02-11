using SiteLeiloes.Models;

namespace SiteLeiloes.Data.Interfaces
{
    public interface IUtilizadorRepository
    {
        bool SaveChanges();
        IEnumerable<Utilizador> GetAll();
        Utilizador GetById(int id);
        Utilizador GetByUsername(string username);
        bool Create(Utilizador utilizador);
        public bool Update(Utilizador utilizador);
        public bool Delete(int id);
        public Utilizador GetByCredentials(string username, string password);
    }
}

