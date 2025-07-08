using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class ProyectoDto
    {
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe proporcionar una empresa válida.")]
        public int EmpresaId { get; set; }
    }
}