using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 
using SiteLeiloes.Data;
using SiteLeiloes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SiteLeiloes.Pages.Utilizador
{
    public class LicitacoesUserModel : PageModel
    {
        private readonly CarenseDBContext _context;

        public LicitacoesUserModel(CarenseDBContext context)
        {
            _context = context;
        }

        public int UserId { get; private set; }

        public List<Leilao> Leiloes { get; set; }

        public IActionResult OnGet(string marca, string ordenacao)
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

            UserId = userId;            

            var licitacaoIds = _context.Licitacao
                .Where(lf => lf.UtilizadorId == userId)
                .Select(lf => lf.LeilaoId)
                .ToList();

            Leiloes = _context.Leilao
                .Where(leilao => licitacaoIds.Contains(leilao.Id))
                .Include(leilao => leilao.Carro) 
                .ToList();

            var query = Leiloes.AsQueryable();

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(leilao => leilao.Carro.Marca.ToUpper().Contains(marca.ToUpper()));
            }

            switch (ordenacao)
            {
                case "dataCrescente":
                    query = query.OrderBy(leilao => leilao.Data_de_fim);
                    break;
                case "dataDecrescente":
                    query = query.OrderByDescending(leilao => leilao.Data_de_fim);
                    break;
                case "valorCrescente":
                    query = query.OrderBy(leilao => leilao.Valor);
                    break;
                case "valorDecrescente":
                    query = query.OrderByDescending(leilao => leilao.Valor);
                    break;
            }

            Leiloes = query.Include(leilao => leilao.Carro).ToList();

            return Page();
        }
    }
}
