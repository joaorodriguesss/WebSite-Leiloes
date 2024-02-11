using SiteLeiloes.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SiteLeiloes.Data;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SiteLeiloes.Pages.Utilizador.AtividadeUser
{
    public class AtividadeUserModel : PageModel
    {
        private readonly CarenseDBContext _context;
        public List<Carro> CarrosVendidos { get; set; }
        public List<Carro> CarrosComprados { get; set; }
        public List<Venda> Vendas { get; set; }
        public List<Venda> Compras { get; set; }

        public AtividadeUserModel(CarenseDBContext context)
        {
            _context = context;
        }

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

            Vendas = _context.Venda
                .Include(venda => venda.Carro)
                .Where(venda => venda.VendedorId == userId)
                .ToList();

            CarrosVendidos = Vendas.Select(venda => venda.Carro).ToList();

            Compras = _context.Venda
                .Include(compra => compra.Carro)
                .Where(compra => compra.ClienteId == userId)
                .ToList();

            CarrosComprados = Compras.Select(compra => compra.Carro).ToList();

            var query = Vendas.AsQueryable();

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(venda => venda.Carro.Marca.ToUpper().Contains(marca.ToUpper()));
            }

            switch (ordenacao)
            {
                case "dataCrescente":
                    query = query.OrderBy(venda => venda.Data_fim);
                    break;
                case "dataDecrescente":
                    query = query.OrderByDescending(venda => venda.Data_fim);
                    break;
                case "valorCrescente":
                    query = query.OrderBy(venda => venda.Preco);
                    break;
                case "valorDecrescente":
                    query = query.OrderByDescending(venda => venda.Preco);
                    break;
            }

            Vendas = query.Include(venda => venda.Carro).ToList();

            return Page();
        }
    }
}
