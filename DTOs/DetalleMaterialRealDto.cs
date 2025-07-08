using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class DetalleMaterialRealDto
    {
        public int MaterialPresupuestoId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo real debe ser mayor o igual a cero.")]
        public decimal CostoReal { get; set; }
    }
}
