using APS.Data.Models;
using APS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class GestionEquiposController : Controller
    {
        private readonly ApdatadbContext _context;

        public GestionEquiposController(ApdatadbContext context)
        {
            _context = context;
        }

        // Acción para seleccionar el equipo y ver su historial
        public IActionResult Index(int? equipoId)
        {
            ViewBag.Equipos = _context.Equipos.ToList();

            if (equipoId.HasValue)
            {
                var historial = _context.HistorialEquipos
                    .Where(h => h.EquipoId == equipoId.Value)
                    .OrderByDescending(h => h.FechaCambio)
                    .ToList();

                ViewBag.EquipoSeleccionado = equipoId.Value;
                return View(historial);
            }

            return View();
        }

        // Acción para agregar un cambio al historial
        [HttpPost]
        public IActionResult AgregarCambio(int equipoId, string descripcionCambio)
        {
            var historial = new HistorialEquipo
            {
                EquipoId = equipoId,
                DescripcionCambio = descripcionCambio
            };

            _context.HistorialEquipos.Add(historial);
            _context.SaveChanges();

            return RedirectToAction("Index", new { equipoId = equipoId });
        }
        // Acción para finalizar la atención del equipo
        public IActionResult FinalizarAtencion(int equipoId)
        {
            TempData["Message"] = "La atención del equipo ha finalizado. El equipo puede ser reintegrado al cliente.";

            return RedirectToAction("Index");
        }

        public IActionResult EliminarCambio(int id)
        {
            var cambio = _context.HistorialEquipos.Find(id);
            if (cambio != null)
            {
                _context.HistorialEquipos.Remove(cambio);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { equipoId = cambio.EquipoId });
        }

    }
}