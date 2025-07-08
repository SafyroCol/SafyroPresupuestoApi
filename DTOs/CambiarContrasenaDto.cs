using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class CambiarContrasenaDto
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        public string ContrasenaActual { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
        public string NuevaContrasena { get; set; }
    }
}
