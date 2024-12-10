using APS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APS.Web.Controllers
{
    public class TiendaController : Controller
    {

        private readonly ApdatadbContext _context;

        public TiendaController(ApdatadbContext context)
        {
            _context = context;
        }
        // GET: TiendaController
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ProductosAll()
        {
            var productos = _context.Producto.ToList();
            return View(productos);
        }

        public ActionResult Carrito()
        {
            return View();
        }

        // GET: TiendaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiendaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TiendaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiendaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TiendaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiendaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}