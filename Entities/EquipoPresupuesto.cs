using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class EquipoPresupuesto
    {
		[Key]
		public Guid Id { get; set; }

		[ForeignKey("Presupuesto")]
		public int PresupuestoId { get; set; }

		[ForeignKey("EquipoDepreciacion")]
		public int EquipoDepreciacionId { get; set; }

		public Presupuestos Presupuesto { get; set; }

		public EquipoDepreciacion EquipoDepreciacion { get; set; }

	}
}