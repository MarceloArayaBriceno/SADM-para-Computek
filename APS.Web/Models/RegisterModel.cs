using System.ComponentModel.DataAnnotations;

namespace APS.Web.Models 
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
