using APS.Data.Models;
using APS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
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

        // Acción para generar el reporte en PDF del historial de un equipo
        public IActionResult GenerarReporteHistorial(int equipoId)
        {
            // Obtener el historial de cambios para el equipo con el ID seleccionado
            var historial = _context.HistorialEquipos
                .Where(h => h.EquipoId == equipoId)
                .OrderByDescending(h => h.FechaCambio)
                .ToList();

            // Generar el PDF utilizando la vista ReporteHistorialPdf.cshtml
            return new ViewAsPdf("ReporteHistorialPdf", historial)
            {
                FileName = $"HistorialEquipo_{equipoId}.pdf"
            };
        }

        // Acción para finalizar la atención del equipo
        public IActionResult FinalizarAtencion(int equipoId)
        {
            TempData["Message"] = "La atención del equipo ha finalizado. El equipo puede ser reintegrado al cliente.";

            return RedirectToAction("Index");
        }
    }
}