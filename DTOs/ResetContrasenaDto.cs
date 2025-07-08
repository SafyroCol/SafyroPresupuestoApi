using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class ResetContrasenaDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string NuevaContrasena { get; set; }
    }
}
