using SiteLeiloes.Models;

namespace SiteLeiloes.Data.Interfaces
{
    public interface IAdministradorRepository
    {
        bool SaveChanges();
        IEnumerable<Administrador> GetAll();
        Administrador GetById(int id);
        bool Create(Administrador coworker);
        public bool Update(Administrador coworker);
        public bool Delete(int id);
    }
}
