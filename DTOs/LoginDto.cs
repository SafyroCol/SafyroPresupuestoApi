using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class LoginDto : IValidatableObject
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; }

        public string Password { get; set; }

        public string? Codigo2FA { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Codigo2FA) && string.IsNullOrWhiteSpace(Password))
            {
                yield return new ValidationResult(
                    "Debe ingresar la contraseña si no ha ingresado el código de verificación 2FA.",
                    new[] { nameof(Password) });
            }

            if (!string.IsNullOrWhiteSpace(Codigo2FA) && string.IsNullOrWhiteSpace(Username))
            {
                yield return new ValidationResult(
                    "Debe ingresar el nombre de usuario junto con el código 2FA.",
                    new[] { nameof(Username) });
            }
        }
    }
}
