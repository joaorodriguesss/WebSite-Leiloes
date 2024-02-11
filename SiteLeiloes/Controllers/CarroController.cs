using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SiteLeiloes.Models; 
using SiteLeiloes.Data.Components; 
using SiteLeiloes.Data;
using SiteLeiloes.Data.Interfaces;
namespace SiteLeiloes.Controllers
{
    [ApiController]
    [Route("api/carro")]
    public class CarroController : ControllerBase
    {
        private readonly ICarroRepository _repository;

        public CarroController(ICarroRepository repository)
        {
            _repository = repository;
        }

        // GET api/carro
        [HttpGet]
        public ActionResult<IEnumerable<Carro>> GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }


        // GET api/carro/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Carro>> GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST api/carro
        [HttpPost("{id}")]
        public ActionResult<Carro> Create(Carro carro)
        {
            try
            {
                _repository.Create(carro);
                _repository.SaveChanges();
                return Ok(carro);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //PUT api/carro/{id}
        [HttpPut("{id}")]
        public ActionResult<Carro> Update(int id, Carro carro)
        {
            try
            {
                var existingCarro = _repository.GetById(id);
                if (existingCarro == null)
                {
                    return NotFound();
                }

                existingCarro.Marca = carro.Marca;
                existingCarro.Modelo = carro.Modelo;
                existingCarro.Ano = carro.Ano;
                existingCarro.Km = carro.Km;
                existingCarro.Condicao = carro.Condicao;

                _repository.Update(existingCarro);
                _repository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //DELETE api/carro/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var carro = _repository.GetById(id);
            if (carro == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
