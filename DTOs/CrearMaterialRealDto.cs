using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class CrearMaterialRealDto
    {
        public Guid PresupuestoId { get; set; }

        [Required(ErrorMessage = "Debe indicar los materiales y sus costos reales.")]
        public List<DetalleMaterialRealDto> Detalles { get; set; }
    }
}
