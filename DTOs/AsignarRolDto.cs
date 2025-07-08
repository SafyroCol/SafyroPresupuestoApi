using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class AsignarRolDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; }
    }
}
