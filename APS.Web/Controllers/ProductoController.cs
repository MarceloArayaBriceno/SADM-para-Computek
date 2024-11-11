using APS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APS.Web.Controllers
{
    public class ProductoController : Controller // Cambié la sintaxis para la herencia de Controller
    {
        private readonly ApdatadbContext _context;

        // Constructor para inicializar el contexto
        public ProductoController(ApdatadbContext context)
        {
            _context = context;
        }

        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producto.ToListAsync());
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.IdProducto == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto viewModelProducto)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    IdProducto = viewModelProducto.IdProducto,
                    Nombre = viewModelProducto.Nombre,
                    Descripcion = viewModelProducto.Descripcion,
                    Precio = viewModelProducto.Precio,
                    RutaImagen = viewModelProducto.RutaImagen,
                    Stock = viewModelProducto.Stock,
                    FechaRegistro = DateTime.Now
                };

                _context.Producto.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Faltaba el retorno aquí
            }

            return View(viewModelProducto);
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto viewModelProducto)
        {
            if (id != viewModelProducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productoExistente = await _context.Producto.FindAsync(id);
                    if (productoExistente == null)
                    {
                        return NotFound();
                    }

                    // Actualizamos las propiedades
                    productoExistente.Nombre = viewModelProducto.Nombre;
                    productoExistente.Descripcion = viewModelProducto.Descripcion;
                    productoExistente.Precio = viewModelProducto.Precio;
                    productoExistente.Stock = viewModelProducto.Stock;
                    productoExistente.RutaImagen = viewModelProducto.RutaImagen;
                    productoExistente.activo = viewModelProducto.activo;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(viewModelProducto);
        }

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Producto.Any(e => e.IdProducto == id);
        }
    }
}
