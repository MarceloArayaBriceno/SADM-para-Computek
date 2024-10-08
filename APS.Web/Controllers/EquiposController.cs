using Microsoft.AspNetCore.Mvc;
using APS.Data.Models;
using APS.Web.Models;
using APS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Rotativa.AspNetCore;
using APS.Web.Filters;

namespace APS.Web.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class EquiposController : Controller
    {

        private readonly ApdatadbContext _context;

        // Constructor con inyección de dependencias
        public EquiposController(ApdatadbContext context)
        {
            _context = context;
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                // Asignar un valor de UsuarioId si no está asignado
                if (!equipo.UsuarioId.HasValue)
                {
                    equipo.UsuarioId = 1; // Valor predeterminado o según lógica de negocio
                }

                try
                {
                    _context.Equipos.Add(equipo);
                    _context.SaveChanges(); // Guarda en la base de datos

                    // Redirige a la misma página con un parámetro para mostrar el modal de éxito
                    return RedirectToAction("Create", new { saved = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
                }
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario con los datos ingresados
            return View(equipo);
        }

        // Método para mostrar la lista de aprobaciones con checkboxes
        [HttpGet]
        public IActionResult VerificarAprobaciones()
        {
            return View(new AprobacionesViewModel());
        }

        // Método para procesar la verificación de aprobaciones
        [HttpPost]
        public IActionResult VerificarAprobaciones(AprobacionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.EstadoFisico && model.MarcaAprobada && model.SinHumedadCalor &&
                    model.ProcesadorCompatible && model.NoObsoleto)
                {
                    // Redirigir al formulario de registro del equipo si todo es válido
                    return RedirectToAction("Create", "Equipos");
                }
                else
                {
                    // Mostrar mensaje de rechazo
                    TempData["Message"] = "El equipo no cumple con todos los requisitos.";
                    return RedirectToAction("EquipoRechazado");
                }
            }

            // Volver a mostrar el formulario si el modelo no es válido
            return View(model);
        }

        // Método para mostrar la vista de equipo rechazado (opcional)
        public IActionResult EquipoRechazado()
        {
            return View();
        }


        // Acción para generar el PDF del reporte de equipos
        public IActionResult DescargarReportePdf()
        {
            // Obtener los equipos de la base de datos
            var equipos = _context.Equipos.ToList();

            // Usar la vista "ReporteEquiposPdf" para generar el PDF
            var pdf = new ViewAsPdf("ReporteEquiposPdf", equipos)
            {
                FileName = "ListadoEquipos.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape, // Opcional: orientación
                PageSize = Rotativa.AspNetCore.Options.Size.A4 // Opcional: tamaño de página
            };

            return pdf;
        }


    }
}