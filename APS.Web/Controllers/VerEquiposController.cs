using APS.Data.Models;
using APS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class VerEquiposController : Controller
    {
        private readonly ApdatadbContext _context;

        public VerEquiposController(ApdatadbContext context)
        {
            _context = context;
        }

        // Acción para listar los equipos con búsqueda
        public async Task<IActionResult> Index(string searchId, string searchCliente)
        {
            // Obtener todos los equipos
            var equipos = _context.Equipos.AsQueryable();

            // Filtrar por ID si se proporciona
            if (!string.IsNullOrEmpty(searchId))
            {
                if (int.TryParse(searchId, out int id))
                {
                    equipos = equipos.Where(e => e.EquipoId == id);
                }
            }

            // Filtrar por Nombre del Cliente si se proporciona
            if (!string.IsNullOrEmpty(searchCliente))
            {
                equipos = equipos.Where(e => e.NombreCliente.Contains(searchCliente));
            }

            // Ejecutar la consulta y enviar a la vista
            return View(await equipos.ToListAsync());
        }

        // Las otras acciones permanecen igual...
    }
}
