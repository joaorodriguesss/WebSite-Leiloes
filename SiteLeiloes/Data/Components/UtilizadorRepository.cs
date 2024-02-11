using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;

namespace SiteLeiloes.Data.Components
{
    public class UtilizadorRepository : IUtilizadorRepository
    {
        private readonly CarenseDBContext _context;

        public UtilizadorRepository(CarenseDBContext context)
        {
            _context = context;

        }

        public IEnumerable<Utilizador> GetAll()
        {
            return _context.Utilizador.ToList();
        }

        public Utilizador GetById(int id)
        {
            return _context.Utilizador.FirstOrDefault(u => u.Id == id);
        }

		public Utilizador GetByUsername(string username)
		{
			return _context.Utilizador.FirstOrDefault(u => u.Username == username);
		}

		public bool Create(Utilizador utilizador)
        {
            try
            {
                utilizador.Password = BCrypt.Net.BCrypt.HashPassword(utilizador.Password);
                _context.Utilizador.Add(utilizador);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool Update(Utilizador utilizador)
        {
            try
            {
                _context.Entry(utilizador).State = EntityState.Modified;
                return SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var utilizador = _context.Utilizador.FirstOrDefault(u => u.Id == id);
                if (utilizador == null)
                {
                    return false;
                }

                _context.Utilizador.Remove(utilizador);
                return SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Utilizador GetByCredentials(string username, string password)
        {
            var utilizador = _context.Utilizador
                .FirstOrDefault(u => u.Username == username);

            if (utilizador == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, utilizador.Password))
            {
                return utilizador;
            }

            return null;
        }
      
    }
}
