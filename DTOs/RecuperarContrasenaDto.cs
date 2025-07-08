using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class RecuperarContrasenaDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        public string Email { get; set; }
    }
}
