using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El ID de la empresa es obligatorio.")]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Role { get; set; }
    }
}
