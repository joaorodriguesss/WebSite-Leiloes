using SiteLeiloes.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SiteLeiloes.Data;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class LeiloesFuturosModel : PageModel
{
    private readonly CarenseDBContext _context;

    public LeiloesFuturosModel(CarenseDBContext context)
    {
        _context = context;
    }

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

        Leiloes = _context.Leilao
            .Where(leilao => DateTime.Now <= leilao.Data_de_inicio)
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
