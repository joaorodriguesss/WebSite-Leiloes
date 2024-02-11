using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteLeiloes.Data.Interfaces
{
    public interface ILeilaoRepository
    {
        Task<IEnumerable<Leilao>> GetAllAsync();
        Task<Leilao> GetByIdAsync(int id);
        Task<bool> CreateAsync(Leilao leilao);
        Task<bool> UpdateAsync(Leilao leilao);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<Carro> GetCarroByIdAsync(int carroId);
        Task<bool> AddLicitacaoAsync(Licitacao licitacao);
    }
}

