using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class EmpresaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "El Dominio no puede exceder los 200 caracteres.")]
        public string Dominio { get; set; }
    }
}
