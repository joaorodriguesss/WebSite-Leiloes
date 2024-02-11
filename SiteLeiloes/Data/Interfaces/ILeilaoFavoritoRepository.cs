using SiteLeiloes.Models;

namespace SiteLeiloes.Data.Interfaces
{
    public interface ILeilaoFavoritoRepository
    {
        bool SaveChanges();
        IEnumerable<LeilaoFavorito> GetAll();
        LeilaoFavorito GetById(int id);
        bool Create(LeilaoFavorito leilaofavorito);
        public bool Update(LeilaoFavorito leilaofavorito);
        public bool Delete(int id);
        public bool DeleteByLeilaoAndUserId(int leilaoId, int userId);
    }
}
