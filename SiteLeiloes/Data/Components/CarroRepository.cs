using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteLeiloes.Data.Components
{
    public class CarroRepository : ICarroRepository
    {
        private readonly CarenseDBContext _context;
        private IEnumerable<Carro> _carro = new List<Carro>();
        public CarroRepository(CarenseDBContext context)
        {       
            _context = context;
        }

        public IEnumerable<Carro> GetAll()
        {
            return _context.Carro.ToList();
        }


        public Carro? GetById(int id)
        {
            return _context.Carro.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Carro> GetByIdAsync(int id)
        {
            return await _context.Carro.FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Create(Carro carro)
        {
            try
            {
                _context.Carro.Add(carro);
                _context.SaveChanges();
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

        public bool Update(Carro carro)
        {
            try
            {
                _context.Entry(carro).State = EntityState.Modified;
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
                var carro = _context.Carro.FirstOrDefault(c => c.Id == id);
                if (carro == null)
                {
                    return false;
                }

                _context.Carro.Remove(carro);
                return SaveChanges();
            }
            catch (Exception)
            {               
                return false;
            }
        }
    }
}