using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace SiteLeiloes.Pages.Utilizador
{
    public class CarroModel : PageModel
    {
        [BindProperty]
        public float PrecoMinimo { get; set; }

        [BindProperty]
        public DateTime DataDeInicio { get; set; }

        [BindProperty]
        public DateTime DataDeFim { get; set; }


        public CarroViewModel Carro { get; set; }
        private readonly ICarroRepository _repository;
        private readonly ILeilaoRepository _leilaoRepository;
        public CarroModel(ICarroRepository repository, ILeilaoRepository leilaoRepository)
        {
            _repository = repository;
            _leilaoRepository = leilaoRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("AcessoNegado"); 
            }

            if (!int.TryParse(userIdString, out int userId) || userId == 0)
            {
                return RedirectToPage("AcessoNegado"); 
            }

            var carroEntity = await _repository.GetByIdAsync(id);
            if (carroEntity == null)
            {
                return NotFound();
            }

            if (carroEntity.UserId != userId)
            {
                return RedirectToPage("AcessoNegado"); 
            }

            Carro = new CarroViewModel
            {
                Id = carroEntity.Id,
                MarcaCarro = carroEntity.Marca,
                ModeloCarro = carroEntity.Modelo,
                AnoCarro = carroEntity.Ano,
                KmCarro = carroEntity.Km,
                CondicaoCarro = carroEntity.Condicao,
                ImagemCarro = carroEntity.ImagemUrl,
            };

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int carroid)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            var carroEntity = await _repository.GetByIdAsync(carroid);
            Debug.WriteLine($"Onpost {carroid}");
            var leilao = new Leilao
            {
                CarroId = carroid, 
                ClienteId = int.Parse(HttpContext.Session.GetString("UserId")), 
                VendedorId = int.Parse(HttpContext.Session.GetString("UserId")),
                Preco_minimo = PrecoMinimo,
                Valor = PrecoMinimo,
                Data_de_inicio = DataDeInicio,
                Data_de_fim = DataDeFim,
                ImagemUrl = carroEntity.ImagemUrl
            };


            var success = await _leilaoRepository.CreateAsync(leilao);
            if (!success)
            {
                return Page();
            }

            return RedirectToPage("PaginaGaragem"); 
        }

    }
}


