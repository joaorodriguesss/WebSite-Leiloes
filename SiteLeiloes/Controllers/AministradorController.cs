using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SiteLeiloes.Models; 
using SiteLeiloes.Data.Components; 
using SiteLeiloes.Data;
using SiteLeiloes.Data.Interfaces;
namespace SiteLeiloes.Controllers
{
    [ApiController]
    [Route("api/administrador")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorRepository _repository;

        public AdministradorController(IAdministradorRepository repository)
        {
            _repository = repository;
        }

        // GET api/administrador
        [HttpGet]
        public ActionResult<IEnumerable<Administrador>> GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }


        // GET api/administrador/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Administrador>> GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST api/administrador
        [HttpPost("{id}")]
        public ActionResult<Administrador> Create(Administrador administrador)
        {
            try
            {
                _repository.Create(administrador);
                _repository.SaveChanges();
                return Ok(administrador);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //PUT api/administrador/{id}
        [HttpPut("{id}")]
        public ActionResult<Administrador> Update(int id, Administrador administrador)
        {
            try
            {
                var existingAdministrador = _repository.GetById(id);
                if (existingAdministrador == null)
                {
                    return NotFound();
                }

                existingAdministrador.Username = administrador.Username;
                existingAdministrador.Password = administrador.Password;

                _repository.Update(existingAdministrador);
                _repository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //DELETE api/coworkor/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var administrador = _repository.GetById(id);
            if (administrador == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
