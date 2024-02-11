using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SiteLeiloes.Models; 
using SiteLeiloes.Data.Components; 
using SiteLeiloes.Data;
using SiteLeiloes.Data.Interfaces;
namespace SiteLeiloes.Controllers
{
    [ApiController]
    [Route("api/leilaofavorito")]
    public class LeilaoFavoritoController : ControllerBase
    {
        private readonly ILeilaoFavoritoRepository _repository;

        public LeilaoFavoritoController(ILeilaoFavoritoRepository repository)
        {
            _repository = repository;
        }

        // GET api/leilaofavorito
        [HttpGet]
        public ActionResult<IEnumerable<LeilaoFavorito>> GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }


        // GET api/leilaofavorito/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<LeilaoFavorito>> GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST api/leilaofavorito
        [HttpPost("{id}")]
        public ActionResult<LeilaoFavorito> Create(LeilaoFavorito leilaofavorito)
        {
            try
            {
                _repository.Create(leilaofavorito);
                _repository.SaveChanges();
                return Ok(leilaofavorito);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //PUT api/leilaofavorito/{id}
        [HttpPut("{id}")]
        public ActionResult<LeilaoFavorito> Update(int id, LeilaoFavorito leilaofavorito)
        {
            try
            {
                var existingLeilaoFavorito = _repository.GetById(id);
                if (existingLeilaoFavorito == null)
                {
                    return NotFound();
                }

                existingLeilaoFavorito.LeilaoId = leilaofavorito.LeilaoId;
                existingLeilaoFavorito.UtilizadorId = leilaofavorito.UtilizadorId;

                _repository.Update(existingLeilaoFavorito);
                _repository.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //DELETE api/coworkor/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var leilaofavorito = _repository.GetById(id);
            if (leilaofavorito == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}