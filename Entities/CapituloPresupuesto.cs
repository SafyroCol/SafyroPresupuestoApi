namespace SafyroPresupuestos.Entities
{
	public class CapituloPresupuesto
	{
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public string Codigo { get; set; }  // Ej: 1.1, 2.3
		public string Nombre { get; set; }

		public decimal SubtotalMateriales { get; set; }
		public decimal SubtotalManoObra { get; set; }
		public decimal SubtotalEquipos { get; set; }
		public decimal SubtotalOtros { get; set; }
		public decimal TotalCapitulo { get; set; }

		public virtual List<PartidaPresupuesto> Partidas { get; set; }
	}

}
