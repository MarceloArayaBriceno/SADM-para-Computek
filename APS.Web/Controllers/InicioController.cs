using Microsoft.AspNetCore.Mvc;

namespace APS.Web.Controllers
{
    // Controlador para la pantalla de inicio
    public class InicioController : Controller
    {
        // Acción para mostrar la vista de inicio
        public IActionResult Index()
        {
            // Retorna la vista "Index" de la carpeta "Views/Inicio"
            return View();
        }
    }
}
