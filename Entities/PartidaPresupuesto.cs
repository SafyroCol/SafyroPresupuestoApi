using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
	public class PartidaPresupuesto
	{
		public Guid Id { get; set; }
		public int PresupuestoId { get; set; }
		public Presupuestos Presupuesto { get; set; }
		public Guid PartidaId { get; set; }
		public Partida Partida { get; set; }
	}

}
