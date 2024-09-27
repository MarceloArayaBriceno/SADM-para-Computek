using APS.Data.Models;
using APS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Acción para listar los equipos
        public async Task<IActionResult> Index()
        {
            var equipos = await _context.Equipos.ToListAsync();
            return View(equipos);
        }

        // Acción para editar un equipo (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        // Acción para manejar la edición (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoId,Marca,Modelo,NombreCliente,MotivoIngreso,GarantiaConLocal,ContraseñaEquipo,Descripcion,FechaIngreso,UsuarioId")] Equipo equipo)
        {
            if (id != equipo.EquipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.EquipoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(equipo);
        }

        // Acción para eliminar un equipo (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // Acción para confirmar la eliminación (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método para verificar si el equipo existe
        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.EquipoId == id);
        }
    }
}
