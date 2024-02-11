using SiteLeiloes.Models;

namespace SiteLeiloes.Data.Interfaces
{
    public interface IVendaRepository
    {
        bool SaveChanges();
        IEnumerable<Venda> GetAll();
        Venda GetById(int id);
        bool Create(Venda venda);
        public bool Update(Venda venda);
        public bool Delete(int id);
    }
}
