using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using SiteLeiloes.Models;
using SiteLeiloes.Data.Components; 
using SiteLeiloes.Data.Interfaces;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SiteLeiloes.Pages.Utilizador
{
    public class PaginaUserModel : PageModel
    {
        private readonly IUtilizadorRepository _utilizadorRepository;

        public Models.Utilizador Utilizador { get; set; }
        public PaginaUserModel(IUtilizadorRepository utilizadorRepository)
        {
            _utilizadorRepository = utilizadorRepository;
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

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out _))
            {
                Utilizador = _utilizadorRepository.GetById(userId);
            }
            else
            {

            }
            return Page();
        }
    }
}

