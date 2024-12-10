using Microsoft.AspNetCore.Mvc;

namespace APS.Web.Controllers
{
    public class MantenimientosController : Controller
    {
        // Acción para mostrar la vista de mantenimiento
        public IActionResult Index()
        {
            return View();
        }
    }
}
