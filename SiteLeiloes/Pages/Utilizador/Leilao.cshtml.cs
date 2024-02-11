using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Data;
using System;


namespace SiteLeiloes.Pages.Utilizador
{
    public class LeilaoModel : PageModel
    {
        public LeilaoViewModel Leilao { get; set; }
        private readonly CarenseDBContext _context;
        private readonly ILeilaoRepository _repository;
        private readonly ILeilaoFavoritoRepository _favoritoRepository;

        public LeilaoModel(CarenseDBContext context, ILeilaoRepository repository, ILeilaoFavoritoRepository favoritoRepository)
        {
            _context = context;
            _repository = repository;
            _favoritoRepository = favoritoRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var leilaoEntity = await _repository.GetByIdAsync(id);

            if (DateTime.Now < leilaoEntity.Data_de_inicio)
            {
                ModelState.AddModelError("", "Não é possível licitar em um leilão que ainda não começou.");
                return RedirectToPage("LeiloesFuturos");
            }

            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("AcessoNegado"); 
            }

            if (!int.TryParse(userIdString, out int userId) || userId == 0)
            {
                return RedirectToPage("AcessoNegado"); 
            }

            if (leilaoEntity == null)
            {
                return NotFound();
            }
            var carroEntity = await _repository.GetCarroByIdAsync(leilaoEntity.CarroId);
            if (carroEntity == null)
            {
                return NotFound("Carro nao encontrado.");
            }

            Leilao = new LeilaoViewModel
            {
                Id = leilaoEntity.Id,
                Preco_minimo = leilaoEntity.Preco_minimo,
                Valor = leilaoEntity.Valor,
                VendedorId = leilaoEntity.VendedorId,
                CarroId = leilaoEntity.CarroId,
                Data_de_inicio = leilaoEntity.Data_de_inicio,
                Data_de_fim = leilaoEntity.Data_de_fim,
                ImagemUrl = leilaoEntity.ImagemUrl,
                MarcaCarro = carroEntity.Marca,
                ModeloCarro = carroEntity.Modelo,
                AnoCarro = carroEntity.Ano,
                KmCarro = carroEntity.Km,
                CondicaoCarro = carroEntity.Condicao,
                ImagemCarro = carroEntity.ImagemUrl,
            };

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostToggleFavoriteAsync(int leilaoId)
        {
            try
            {

                var leilaoExists = _context.Leilao.Any(l => l.Id == leilaoId);
                if (!leilaoExists)
                {

                    return NotFound($"Leilão com ID {leilaoId} não encontrado.");
                }

                var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.Parse(userIdValue);

                var favoritoExistente = _favoritoRepository.GetAll().FirstOrDefault(f => f.LeilaoId == leilaoId && f.UtilizadorId == userId);

                if (favoritoExistente != null)
                {
                    _favoritoRepository.Delete(favoritoExistente.Id);
                }
                else
                {
                    var novoFavorito = new LeilaoFavorito(leilaoId, userId);
                    _favoritoRepository.Create(novoFavorito);
                }

                _favoritoRepository.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [BindProperty]
        public float BidValue { get; set; } 

        public async Task<IActionResult> OnPostAsync(int id)
        {



            var leilaoEntity = await _repository.GetByIdAsync(id);
            if (leilaoEntity == null)
            {
                return NotFound();
            }

            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (leilaoEntity.VendedorId.ToString() == userIdValue)
            {
                ModelState.AddModelError("", "O vendedor não pode licitar no próprio leilão.");
                return RedirectToPage("PagInicial");
            }
            

            if (BidValue > leilaoEntity.Valor)
            {
                leilaoEntity.Valor = BidValue;
                leilaoEntity.ClienteId = int.Parse(userIdValue);

                var licitacao = new Licitacao(id, int.Parse(userIdValue));
                await _repository.AddLicitacaoAsync(licitacao);

                await _repository.UpdateAsync(leilaoEntity);
                await _repository.SaveChangesAsync();

                return RedirectToPage(new { id = id });
            }
            else
            {
                ModelState.AddModelError("BidValue", "A licitação deve ser maior que a licitação atual.");
                return Page();
            }
        }       

    }

}