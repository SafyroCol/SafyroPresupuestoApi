using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class CostosUnitariosDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del costo unitario es obligatorio.")]
        public string Nombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser un número positivo.")]
        public decimal Valor { get; set; }
    }

}
