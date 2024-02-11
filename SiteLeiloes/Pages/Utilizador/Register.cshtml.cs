using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Diagnostics;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly IUtilizadorRepository _utilizadorRepository;

    [BindProperty]
    public Utilizador novoUtilizador { get; set; }

    [BindProperty]
    public string ConfirmPassword { get; set; }
    public RegisterModel(IUtilizadorRepository utilizadorRepository)
    {
        _utilizadorRepository = utilizadorRepository;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (ModelState.IsValid)
        {
            Debug.WriteLine("Onpost model valid");
            _utilizadorRepository.Create(novoUtilizador);
            _utilizadorRepository.SaveChanges();
        }

        return new JsonResult(new { success = true });
    }
}