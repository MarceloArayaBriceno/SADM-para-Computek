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
            var equipos = _context.Equipos.AsQueryable();

            if (!string.IsNullOrEmpty(searchId) && int.TryParse(searchId, out int id))
            {
                equipos = equipos.Where(e => e.EquipoId == id);
            }

            if (!string.IsNullOrEmpty(searchCliente))
            {
                equipos = equipos.Where(e => e.NombreCliente.Contains(searchCliente));
            }

            return View(await equipos.ToListAsync());
        }

        // Acción para editar un equipo
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Equipo equipo)
        {
            if (id != equipo.EquipoId)
            {
                return BadRequest();
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
                    if (!_context.Equipos.Any(e => e.EquipoId == id))
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


        // Acción para eliminar un equipo
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
