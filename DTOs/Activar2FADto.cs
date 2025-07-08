using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class Activar2FADto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }
    }

}
