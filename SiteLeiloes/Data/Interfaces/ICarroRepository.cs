using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteLeiloes.Data.Interfaces
{
    public interface ICarroRepository
    {
        bool SaveChanges();
        IEnumerable<Carro> GetAll();
        Carro GetById(int id);
        Task<Carro> GetByIdAsync(int id);
        bool Create(Carro carro);
        public bool Update(Carro carro);
        public bool Delete(int id);
    }
}
