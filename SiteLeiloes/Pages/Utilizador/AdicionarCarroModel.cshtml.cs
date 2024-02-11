using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Security.Claims;
using System.Diagnostics;

public class AdicionarCarroModel : PageModel
{
    [FromForm]
    public Carro NovoCarro { get; set; }

    private readonly ICarroRepository _carroRepository;

    public AdicionarCarroModel(ICarroRepository carroRepository)
    {
        _carroRepository = carroRepository;
    }

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

        return Page();
    }

    public IActionResult OnPost()
    {
        Debug.WriteLine("OnPost");

        if (!ModelState.IsValid)
        {
            Debug.WriteLine("ModelState is invalid");
            return Page();
        }

        if (User.Identity.IsAuthenticated)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out var userId))
            {
                NovoCarro.UserId = userId;
            }
            else
            {
                Debug.WriteLine("User ID is not available or invalid");
                return Page();
            }
        }
        else
        {
            Debug.WriteLine("User is not authenticated");
            return RedirectToPage("/Login");
        }

        var sucesso = _carroRepository.Create(NovoCarro);

        if (!sucesso)
        {
            Debug.WriteLine("Failed to create a new car");
            return Page();
        }
        Debug.WriteLine("Car added successfully " + NovoCarro.Ano);

        return new JsonResult(new { success = true, message = "Car added successfully", ano = NovoCarro.Ano });
    }
}
