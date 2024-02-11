using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteLeiloes.Data.Components
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly CarenseDBContext _context;
        private IEnumerable<Administrador> _administrador = new List<Administrador>();
        public AdministradorRepository(CarenseDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Administrador> GetAll()
        {
            return _context.Administrador.ToList();
        }


        public Administrador GetById(int id)
        {
            return _context.Administrador.FirstOrDefault(c => c.Id == id);
        }

        public bool Create(Administrador administrador)
        {
            try
            {
                _context.Administrador.Add(administrador);
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

        public bool Update(Administrador administrador)
        {
            try
            {
                _context.Entry(administrador).State = EntityState.Modified;
                return SaveChanges();
            }
            catch (Exception)
            {
                // Log the exception
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var administrador = _context.Administrador.FirstOrDefault(c => c.Id == id);
                if (administrador == null)
                {
                    return false;
                }

                _context.Administrador.Remove(administrador);
                return SaveChanges();
            }
            catch (Exception)
            {
                // Log the exception
                return false;
            }
        }
    }
}


