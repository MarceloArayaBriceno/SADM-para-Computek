/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APS.Web.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Verify if the user is logged in
        context.Result = new RedirectToActionResult("Index", "Login", null);
    }
}
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APS.Web.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verifica si el usuario está autenticado
            var userEmail = context.HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // Redirige a la página de login si no hay sesión de usuario
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            // Si el usuario está autenticado, no hagas nada y permite el acceso
        }
    }
}
