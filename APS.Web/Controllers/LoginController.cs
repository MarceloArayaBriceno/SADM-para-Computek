
using APS.Security;
using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;

/*
namespace APS.Web.Controllers
{
    public class LoginController(ISecurityService service) : Controller
    {
        private readonly ISecurityService _service = service;

        public IActionResult Index()
        {
            return View("/Views/Accounts/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            bool result = false;
            if (ModelState.IsValid)
                result = await _service.AuthUserByEmailAsync(new Data.Models.User { Email = model.Email, Password = model.Password });

            if (result)
                return RedirectToAction("Index", "Home");
            
            throw new Exception("Error");
        }
    }
}

*/
namespace APS.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISecurityService _service;

        // Constructor que inyecta el servicio de seguridad
        public LoginController(ISecurityService service)
        {
            _service = service;  // Inicializa el campo del servicio con la dependencia inyectada
        }

        // Método para mostrar la vista de login
        public IActionResult Index()
        {
            return View("/Views/Accounts/Login.cshtml");
        }

        // Método para manejar el intento de login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Imprime un mensaje en la consola para depurar
                Console.WriteLine("ModelState es válido. Intentando autenticar...");

                // Intenta autenticar al usuario
                bool isAuthenticated = await _service.AuthUserByEmailAsync(new Data.Models.User { Email = model.Email, Password = model.Password });

                if (isAuthenticated)
                {
                    Console.WriteLine($"Usuario {model.Email} autenticado correctamente.");

                    // Almacena la información del usuario en la sesión
                    HttpContext.Session.SetString("UserEmail", model.Email);

                    // Redirige al usuario al Home/Index si la autenticación es exitosa
                    return RedirectToAction("Index", "Factura");
                }
                else
                {
                    // Si la autenticación falla, añade un error al ModelState para informar al usuario
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    Console.WriteLine($"No se pudo autenticar al usuario con el email: {model.Email}. Verifica las credenciales.");
                }
            }
            else
            {
                // Imprime un mensaje en la consola para depurar
                Console.WriteLine("ModelState no es válido. Errores:");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

            }

            // Devuelve la misma vista de login con el modelo actual y cualquier error de ModelState
            return View("/Views/Accounts/Login.cshtml", model);
        }

        // Método para cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpia la sesión
            return RedirectToAction("Index", "Tienda");
        }
    }
}
