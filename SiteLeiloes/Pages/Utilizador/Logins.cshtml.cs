#define DEBUG

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteLeiloes.Data.Interfaces;
using SiteLeiloes.Models;
using System.Diagnostics;

namespace SiteLeiloes.Pages.Utilizador
{
    [AllowAnonymous]
    public class LoginsModel : PageModel
    {
        public class LoginInputModel
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private readonly IUtilizadorRepository _utilizadorRepository;

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public LoginsModel(IUtilizadorRepository utilizadorRepository)
        {
            _utilizadorRepository = utilizadorRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Debug.WriteLine("Onpost");
            var result = await ProcessarAutenticacaoAsync();
            Debug.WriteLine("Result: " + result);
            return result;
        }

        private async Task<IActionResult> ProcessarAutenticacaoAsync()
        {
            Debug.WriteLine("Processar ");
            var utilizador = _utilizadorRepository.GetByCredentials(Input.Username, Input.Password);

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return Page();
            }

            if (utilizador == null)
            {
                Debug.WriteLine("credencias invalidas");
                ModelState.AddModelError(string.Empty, "Credenciais inv�lidas");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilizador.Username),
                new Claim(ClaimTypes.NameIdentifier, utilizador.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "AuthenticationCookies");
            await HttpContext.SignInAsync("AuthenticationCookies", new ClaimsPrincipal(identity));

            HttpContext.Session.SetString("UserId", utilizador.Id.ToString());

            return RedirectToPage("PagInicial");
        }
    }
}