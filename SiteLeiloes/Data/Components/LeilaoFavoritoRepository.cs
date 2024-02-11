using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteLeiloes.Data.Components
{
    public class LeilaoFavoritoRepository : ILeilaoFavoritoRepository
    {
        private readonly CarenseDBContext _context;
        private IEnumerable<LeilaoFavorito> _leilaofavorito = new List<LeilaoFavorito>();
        public LeilaoFavoritoRepository(CarenseDBContext context)
        {
            _context = context;
        }

        public IEnumerable<LeilaoFavorito> GetAll()
        {
            return _context.LeilaoFavorito.ToList();
        }


        public LeilaoFavorito? GetById(int id)
        {
            return _context.LeilaoFavorito.FirstOrDefault(c => c.Id == id);
        }

        public bool Create(LeilaoFavorito leilaofavorito)
        {
            try
            {
                _context.LeilaoFavorito.Add(leilaofavorito);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool Update(LeilaoFavorito leilaofavorito)
        {
            try
            {
                _context.Entry(leilaofavorito).State = EntityState.Modified;
                return SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var leilaofavorito = _context.LeilaoFavorito.FirstOrDefault(c => c.Id == id);
                if (leilaofavorito == null)
                {
                    return false;
                }

                _context.LeilaoFavorito.Remove(leilaofavorito);
                return SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteByLeilaoAndUserId(int leilaoId, int userId)
        {
            try
            {
                var leilaofavorito = _context.LeilaoFavorito.FirstOrDefault(c => c.LeilaoId == leilaoId && c.UtilizadorId == userId);
                if (leilaofavorito != null)
                {
                    _context.LeilaoFavorito.Remove(leilaofavorito);
                    return SaveChanges();
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}