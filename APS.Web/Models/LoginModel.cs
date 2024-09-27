
using APS.Web.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

/*
namespace APS.Web.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    //[BindNever]
    //[ValidationsFilter]
    public string Password { get; set; }
}
*/

namespace APS.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string Password { get; set; }

        // Nueva propiedad RememberMe
        public bool RememberMe { get; set; } = false;
    }
}
