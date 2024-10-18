/*using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using APS.Web.Filters;


namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

*/

using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using APS.Web.Filters;

namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //tarea 4.5 - Método Index que verifica si el usuario está autenticado
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail"); // Obtiene el correo electrónico del usuario desde la sesión
            if (string.IsNullOrEmpty(userEmail))
            {
                // Redirige al usuario a la página de login si no está autenticado
                return RedirectToAction("Index", "Login");
            }

            // Lógica del controlador para usuarios autenticados...
            return View();
        }

        //tarea 4.5 - Método Privacy que también puede requerir autenticación
        public IActionResult Privacy()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail"); // Verifica si el usuario está autenticado
            if (string.IsNullOrEmpty(userEmail))
            {
                // Redirige al usuario a la página de login si no está autenticado
                return RedirectToAction("Index", "Login");
            }

            // Lógica del controlador para usuarios autenticados...
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
