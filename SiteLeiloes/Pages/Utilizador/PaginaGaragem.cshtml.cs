using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteLeiloes.Data;
using SiteLeiloes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class PaginaGaragemModel : PageModel
{
    private readonly CarenseDBContext _context;

    public PaginaGaragemModel(CarenseDBContext context)
    {
        _context = context;
    }

    public List<Carro> Carros { get; set; }
    public List<Leilao> Leiloes { get; set; }

    public IActionResult OnGet()
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
            .Where(leilao => leilao.VendedorId == userId)
            .Include(leilao => leilao.Carro) 
            .ToList();

        Carros = _context.Carro
            .Where(carro => carro.UserId == userId && !_context.Leilao.Any(leilao => leilao.CarroId == carro.Id))
            .ToList();

        return Page();
    }
}