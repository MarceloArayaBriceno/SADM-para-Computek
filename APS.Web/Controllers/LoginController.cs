using APS.Security;
using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace APS.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISecurityService _service;

        public LoginController(ISecurityService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View("/Views/Accounts/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState es válido. Intentando autenticar...");

                var user = await _service.AuthUserByEmailAsync(new Data.Models.User { Email = model.Email, Password = model.Password });

                if (user != null && user.Role == 1) // Verifica que el usuario sea admin (Role = 1)
                {
                    Console.WriteLine($"Usuario {model.Email} autenticado correctamente como admin.");

                    HttpContext.Session.SetString("UserEmail", model.Email);
                    HttpContext.Session.SetInt32("UserRole", user.Role);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido o sin permisos de administrador.");
                    Console.WriteLine($"No se pudo autenticar al usuario con el email: {model.Email} o no tiene permisos de admin.");
                }
            }

            return View("/Views/Accounts/Login.cshtml", model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
