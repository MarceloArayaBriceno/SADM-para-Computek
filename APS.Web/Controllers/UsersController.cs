using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APS.Data.Models;
using Microsoft.AspNetCore.Http;

namespace APS.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApdatadbContext _context;

        public UsersController(ApdatadbContext context)
        {
            _context = context;
        }

        // Método para verificar si el usuario está autenticado y tiene el rol de admin
        private bool IsUserAdmin()
        {
            return HttpContext.Session.GetInt32("UserRole") == 1; // Verifica que el rol sea 1 (admin)
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View(await _context.Users.ToListAsync());
        }

        // Resto de acciones (Details, Create, Edit, Delete)
        // En cada acción, comprueba si el usuario es admin:
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
}
