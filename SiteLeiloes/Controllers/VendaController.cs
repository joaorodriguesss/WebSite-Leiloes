using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SiteLeiloes.Models; 
using SiteLeiloes.Data.Components; 
using SiteLeiloes.Data;
using SiteLeiloes.Data.Interfaces;
namespace SiteLeiloes.Controllers
{
    [ApiController]
    [Route("api/venda")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _repository;

        public VendaController(IVendaRepository repository)
        {
            _repository = repository;
        }

        // GET api/venda
        [HttpGet]
        public ActionResult<IEnumerable<Venda>> GetAll()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }


        // GET api/venda/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Venda>> GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST api/venda
        [HttpPost("{id}")]
        public ActionResult<Venda> Create(Venda venda)
        {
            try
            {
                _repository.Create(venda);
                _repository.SaveChanges();
                return Ok(venda);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //PUT api/venda/{id}
        [HttpPut("{id}")]
        public ActionResult<Venda> Update(int id, Venda venda)
        {
            try
            {
                var existingVenda = _repository.GetById(id);
                if (existingVenda == null)
                {
                    return NotFound();
                }

                existingVenda.Data_fim = venda.Data_fim;
                existingVenda.Preco = venda.Preco;
                existingVenda.Vendedor = venda.Vendedor;
                existingVenda.Cliente = venda.Cliente;
                existingVenda.Carro = venda.Carro;

                _repository.Update(existingVenda);
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
            var venda = _repository.GetById(id);
            if (venda == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
